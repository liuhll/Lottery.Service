using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ECommon.Extensions;
using ENode.Commanding;
using Lottery.AppService.LotteryData;
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

        public LotteryController(ILotteryDataAppService lotteryDataAppService,
            ICommandService commandService, 
            INormConfigQueryService normConfigQueryService, 
            ILotteryPredictDataQueryService lotteryPredictDataQueryService) 
            : base(commandService)
        {
            _lotteryDataAppService = lotteryDataAppService;
            _normConfigQueryService = normConfigQueryService;
            _lotteryPredictDataQueryService = lotteryPredictDataQueryService;
        }

        /// <summary>
        /// 计划追号接口
        /// </summary>
        /// <returns>返回计划追号结果</returns>
        [HttpGet]
        [Route("predictdatas")]
        [SwaggerOptionalParameter("predictPeriod")]
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

        private void WritePlanTrackNumbers(IGrouping<string, PredictDataDto> item, PlanInfoDto planInfo, double currentScore)
        {
            var finalPredictData = _lotteryPredictDataQueryService.GetLastPredictData(item.Key, planInfo.PlanNormTable,LotteryInfo.LotteryCode);

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
                    _lotterySession.UserId,planInfo.PlanNormTable,LotteryInfo.LotteryCode,false));
            }
        
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
            foreach (var item in notRunningResult)
            {
                historyPredictResults.Add((int)item.PredictedResult);
            }
            foreach (var item in dbPredictResultData.Safe())
            {
                historyPredictResults.Add((int)item.PredictedResult);
            }
            return historyPredictResults.ToArray();
        }
    }
}
