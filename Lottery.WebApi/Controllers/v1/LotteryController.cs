using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Http;
using ECommon.Extensions;
using ENode.Commanding;
using Lottery.AppService.LotteryData;
using Lottery.AppService.Plan;
using Lottery.Commands.LotteryPredicts;
using Lottery.Dtos.Lotteries;
using Lottery.Dtos.PageList;
using Lottery.QueryServices.Lotteries;

namespace Lottery.WebApi.Controllers.v1
{
    [RoutePrefix("v1/lottery")]
    public class LotteryController : BaseApiV1Controller
    {
        private readonly ILotteryDataAppService _lotteryDataAppService;
        private readonly INormConfigQueryService _normConfigQueryService;
        private readonly ILotteryPredictDataQueryService _lotteryPredictDataQueryService;
        private readonly IPlanTrackAppService _planTrackAppService;

        public LotteryController(ILotteryDataAppService lotteryDataAppService,
            ICommandService commandService, 
            INormConfigQueryService normConfigQueryService, 
            ILotteryPredictDataQueryService lotteryPredictDataQueryService, 
            IPlanTrackAppService planTrackAppService) 
            : base(commandService)
        {
            _lotteryDataAppService = lotteryDataAppService;
            _normConfigQueryService = normConfigQueryService;
            _lotteryPredictDataQueryService = lotteryPredictDataQueryService;
            _planTrackAppService = planTrackAppService;
        }

        /// <summary>
        /// 计划追号接口
        /// </summary>
        /// <returns>返回计划追号结果</returns>
        [HttpGet]
        [Route("predictdatas")]
        public ICollection<PlanTrackNumber> GetPredictDatas()
        {
            var lotteryId = _lotterySession.SystemTypeId;
            var userId = _lotterySession.UserId;
            var data = _lotteryDataAppService.NewLotteryDataList(lotteryId, userId);

            var planTrackNumbers = new List<PlanTrackNumber>();
            data.GroupBy(p => p.NormConfigId).ForEach(item =>
            {
                var planInfo = _normConfigQueryService.GetNormPlanInfoByNormId(item.Key, lotteryId);
                var newestPredictDataDto = item.OrderByDescending(p => p.StartPeriod).First();
                var normConfig = _normConfigQueryService.GetUserNormConfig(item.Key);
                var planTrackNumber = new PlanTrackNumber()
                {
                    NormId = normConfig.Id,
                    PlanId = planInfo.Id,
                    PlanName = planInfo.PlanName,
                    EndPeriod = newestPredictDataDto.EndPeriod,
                    StartPeriod = newestPredictDataDto.StartPeriod,
                    MinorCycle = newestPredictDataDto.MinorCycle,
                    PredictData = newestPredictDataDto.PredictedData,
                    CurrentPredictPeriod = newestPredictDataDto.CurrentPredictPeriod,
                    PredictType = planInfo.DsType,
                    HistoryPredictResults = GetHistoryPredictResults(item.OrderByDescending(p => p.StartPeriod),item.Key, normConfig.LookupPeriodCount, planInfo.PlanNormTable),
                };
                var rightCount = planTrackNumber.HistoryPredictResults.Count(p => p == 0);
                var totleCount = planTrackNumber.HistoryPredictResults.Count(p => p != 2);
                var currentScore = Math.Round((double) rightCount / totleCount,2);
                planTrackNumber.CurrentScore = currentScore;
                WritePlanTrackNumbers(item, planInfo, currentScore);
                planTrackNumbers.Add(planTrackNumber);
            });

            return planTrackNumbers;
        }

        /// <summary>
        /// 切换公式接口(变更计划追号)
        /// </summary>
        /// <returns>返回切换公式后的计划追号</returns>
        [HttpPut]
        [Route("predictdatas")]
        public ICollection<PlanTrackNumber> UpdatePredictDatas()
        {
            return null;
        }

        /// <summary>
        /// 计划追号详情(单个指标)
        /// </summary>
        /// <remarks>获取某个某个用户计划(指标)的详细数据</remarks>
        /// <param name="normId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("predictdetaildata")]
        public PlanTrackDetail GetPredictDetailData(string normId)
        {
            return GetPredictDetailDatas().First(p=>p.NormId == normId);
        }


        /// <summary>
        /// 计划追号详情(All)
        /// </summary>
        /// <remarks> 获取用户指定的所有计划(指标)对应的详细数据</remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("predictdetaildatas")]
        public ICollection<PlanTrackDetail> GetPredictDetailDatas()
        {
            var userNormConfigs =
                _normConfigQueryService.GetUserOrDefaultNormConfigs(LotteryInfo.Id, _lotterySession.UserId);
            var finalLotteryData = _lotteryDataAppService.GetFinalLotteryData(LotteryInfo.Id);
            var predictDetailDatas = new List<PlanTrackDetail>();
            foreach (var userNorm in userNormConfigs)
            {
                var planTrackDetail = _planTrackAppService.GetPlanTrackDetail(userNorm,LotteryInfo.LotteryCode, _lotterySession.UserId);
                predictDetailDatas.Add(planTrackDetail);                
            }
            while (predictDetailDatas.Any(p=>p.CurrentPredictData == null || p.CurrentPredictData.CurrentPredictPeriod < finalLotteryData.NextPeriod))
            {
                GetPredictDatas();
                Thread.Sleep(400);
                predictDetailDatas.Clear();
                foreach (var userNorm in userNormConfigs)
                {
                    var planTrackDetail = _planTrackAppService.GetPlanTrackDetail(userNorm, LotteryInfo.LotteryCode, _lotterySession.UserId);
                    predictDetailDatas.Add(planTrackDetail);
                }
            }
         
            return predictDetailDatas;
        }

        /// <summary>
        /// 获取历史开奖数据
        /// </summary>     
        /// <param name="pageIndex">分页数</param>
        /// <returns>开奖数据</returns>
        [HttpGet]
        [Route("history")]
        public IPageList<LotteryDataDto> List(int pageIndex = 1)
        {
            var lotteryId = _lotterySession.SystemTypeId;
            var list = _lotteryDataAppService.GetList(lotteryId);
            return new PageList<LotteryDataDto>(list,pageIndex);
        }

        /// <summary>
        /// 获取最后一期开奖数据
        /// </summary>
        /// <returns>最后一期开奖数据</returns>
        [HttpGet]
        [Route("finallotterydata")]
        public FinalLotteryDataOutput GetFinalLotteryData()
        {
            var lotteryId = _lotterySession.SystemTypeId;
            return _lotteryDataAppService.GetFinalLotteryData(lotteryId);
        }


        #region private methods 

        private int[] GetHistoryPredictResults(IOrderedEnumerable<PredictDataDto> predictDatas,string normId,int lookupPeriodCount,string planNormTable)
        {
            var historyPredictResults = new List<int>();
            ICollection<PredictDataDto> dbPredictResultData = null;
            var notRunningResult = predictDatas.Where(p => p.PredictedResult != 2).ToList();
            var notRunningResultCount = notRunningResult.Count();
            if (notRunningResultCount < lookupPeriodCount)
            {
                dbPredictResultData =
                    _lotteryPredictDataQueryService.GetNormHostoryPredictDatas(normId, planNormTable, lookupPeriodCount - notRunningResultCount, LotteryInfo.LotteryCode);

            }
            var count = 0;
            foreach (var item in notRunningResult)
            {
                count++;
                historyPredictResults.Add((int)item.PredictedResult);
                if (count >= lookupPeriodCount)
                {
                    break;
                }
            }
            foreach (var item in dbPredictResultData.Safe())
            {
                historyPredictResults.Add((int)item.PredictedResult);
            }
            return historyPredictResults.ToArray();
        }

        private void WritePlanTrackNumbers(IGrouping<string, PredictDataDto> item, PlanInfoDto planInfo, double currentScore)
        {
            var finalPredictData = _lotteryPredictDataQueryService.GetLastPredictData(item.Key, planInfo.PlanNormTable, LotteryInfo.LotteryCode);

            IList<PredictDataDto> needWritePredictDatas = null;
            needWritePredictDatas = finalPredictData != null ?
                item.Where(p => p.StartPeriod >= finalPredictData.StartPeriod).ToList()
                : item.Where(p => true).ToList();

            foreach (var predictData in needWritePredictDatas.Safe())
            {
                SendCommandAsync(new PredictDataCommand(Guid.NewGuid().ToString(), predictData.NormConfigId,
                    predictData.CurrentPredictPeriod, predictData.StartPeriod, predictData.EndPeriod,
                    predictData.MinorCycle, predictData.PredictedData,
                    predictData.PredictedResult, currentScore,
                    _lotterySession.UserId, planInfo.PlanNormTable, LotteryInfo.LotteryCode, false));
            }

        }

        #endregion
    }
}
