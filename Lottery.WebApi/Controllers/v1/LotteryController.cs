using ECommon.Extensions;
using ENode.Commanding;
using Lottery.AppService.LotteryData;
using Lottery.AppService.Plan;
using Lottery.Commands.LotteryPredicts;
using Lottery.Core.Caching;
using Lottery.Dtos.Lotteries;
using Lottery.Dtos.Norms;
using Lottery.Dtos.PageList;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Enums;
using Lottery.QueryServices.Lotteries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Http;
using Lottery.WebApi.Filter;

namespace Lottery.WebApi.Controllers.v1
{
    [RoutePrefix("v1/lottery")]
  
    public class LotteryController : BaseApiV1Controller
    {
        private readonly ILotteryDataAppService _lotteryDataAppService;
        private readonly INormConfigQueryService _normConfigQueryService;
        private readonly ILotteryPredictDataQueryService _lotteryPredictDataQueryService;
        private readonly IPlanTrackAppService _planTrackAppService;
        private readonly ILotteryQueryService _lotteryQueryService;
        private readonly ICacheManager _cacheManager;

        public LotteryController(ILotteryDataAppService lotteryDataAppService,
            ICommandService commandService,
            INormConfigQueryService normConfigQueryService,
            ILotteryPredictDataQueryService lotteryPredictDataQueryService,
            IPlanTrackAppService planTrackAppService,
            ILotteryQueryService lotteryQueryService,
            ICacheManager cacheManager)
            : base(commandService)
        {
            _lotteryDataAppService = lotteryDataAppService;
            _normConfigQueryService = normConfigQueryService;
            _lotteryPredictDataQueryService = lotteryPredictDataQueryService;
            _planTrackAppService = planTrackAppService;
            _lotteryQueryService = lotteryQueryService;
            _cacheManager = cacheManager;
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

            return GetPlanTrackNumberByPredictData(lotteryId, data);
        }

        /// <summary>
        /// 切换公式接口(变更计划追号)
        /// </summary>
        /// <returns>返回切换公式后的计划追号</returns>
        [HttpPut]
        [Route("predictdatas")]
        [AllowAnonymous]
        [AppAuthFilter("您没有切换计划公式的权限,是否购买授权?")]
        public ICollection<PlanTrackNumber> UpdatePredictDatas()
        {
            var lotteryId = _lotterySession.SystemTypeId;
            var userId = _lotterySession.UserId;
            var data = _lotteryDataAppService.UpdateLotteryDataList(lotteryId, userId);
            return GetPlanTrackNumberByPredictData(lotteryId, data);
        }

        /// <summary>
        /// 重新计算单个指标追号数据
        /// </summary>
        /// <returns>提示语</returns>
        [HttpPut]
        [Route("predictdata")]
        [AllowAnonymous]
        [AppAuthFilter("您没有切换计划公式的权限,是否购买授权?")]
        public string UpdatePredictData(UpdatePredictDataInput input)
        {
            var lotteryId = _lotterySession.SystemTypeId;
            var userId = _lotterySession.UserId;
            var data = _lotteryDataAppService.UpdateLotteryDataList(lotteryId, userId, input.NormId);
            GetPlanTrackNumberByPredictData(lotteryId, data);
            return "重新计算追号数据成功";
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
            return GetPredictDetailDatas().First(p => p.NormId == normId);
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

            var cacheKey = string.Format(RedisKeyConstants.LOTTERY_PLANTRACK_DETAIL_KEY, LotteryInfo.Id, _userMemberRank == MemberRank.Ordinary ? LotteryConstants.SystemUser : _lotterySession.UserId, finalLotteryData.Period);
            return _cacheManager.Get<ICollection<PlanTrackDetail>>(cacheKey, () =>
            {
                var predictDetailDatas = new List<PlanTrackDetail>();
                foreach (var userNorm in userNormConfigs)
                {
                    var planTrackDetail = _planTrackAppService.GetPlanTrackDetail(userNorm, LotteryInfo.LotteryCode, _lotterySession.UserId);
                    predictDetailDatas.Add(planTrackDetail);
                }
                while (predictDetailDatas.Any(p => p.CurrentPredictData == null || p.CurrentPredictData.CurrentPredictPeriod < finalLotteryData.NextPeriod))
                {
                    GetPredictDatas();
                    Thread.Sleep(266);
                    predictDetailDatas.Clear();
                    foreach (var userNorm in userNormConfigs)
                    {
                        var planTrackDetail = _planTrackAppService.GetPlanTrackDetail(userNorm, LotteryInfo.LotteryCode, _lotterySession.UserId);
                        predictDetailDatas.Add(planTrackDetail);
                    }
                }

                return predictDetailDatas.OrderBy(p => p.Sort).ToList();
            });
        }

        /// <summary>
        /// 获取历史开奖数据
        /// </summary>
        /// <param name="pageIndex">分页数</param>
        /// <param name="lotteryTime">开奖的时间</param>
        /// <returns>开奖数据</returns>
        [HttpGet]
        [Route("history")]
        public IPageList<LotteryDataDto> List(int pageIndex = 1, DateTime? lotteryTime = null)
        {
            var lotteryId = _lotterySession.SystemTypeId;
            var list = _lotteryDataAppService.GetList(lotteryId, lotteryTime);
            return new DefaultPageList<LotteryDataDto>(list, pageIndex);
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

        /// <summary>
        /// 获取系统内置的所有彩种
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        [AllowAnonymous]
        public ICollection<LotteryInfoOutput> GetAllLotteryInfos()
        {
            var lotteryInfos = _lotteryQueryService.GetAllLotteryInfo();
            return AutoMapper.Mapper.Map<ICollection<LotteryInfoOutput>>(lotteryInfos);
        }

        #region private methods

        private int[] GetHistoryPredictResults(IOrderedEnumerable<PredictDataDto> predictDatas, string normId, int lookupPeriodCount, string planNormTable)
        {
            var historyPredictResults = new List<int>();
            ICollection<PredictDataDto> dbPredictResultData = null;
            var notRunningResult = predictDatas.Where(p => p.PredictedResult != 2).OrderByDescending(p => p.CurrentPredictPeriod).ToList();
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
            historyPredictResults = historyPredictResults.Take(LotteryConstants.HistoryPredictResultCount).ToList();
            historyPredictResults.Reverse();
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

        private ICollection<PlanTrackNumber> GetPlanTrackNumberByPredictData(string lotteryId, IList<PredictDataDto> data)
        {
            var planTrackNumbers = new List<PlanTrackNumber>();
            data.GroupBy(p => p.NormConfigId).ForEach(item =>
            {
                var planInfo = _normConfigQueryService.GetNormPlanInfoByNormId(item.Key, lotteryId);
                var newestPredictDataDto = item.OrderByDescending(p => p.StartPeriod).First();
                var normConfig = _normConfigQueryService.GetUserNormConfig(item.Key);
                var planTrackNumber = new PlanTrackNumber()
                {
                    NormId = normConfig.Id,
                    Sort = normConfig.Sort,
                    PlanId = planInfo.Id,
                    PlanName = planInfo.PlanName,
                    EndPeriod = newestPredictDataDto.EndPeriod,
                    StartPeriod = newestPredictDataDto.StartPeriod,
                    MinorCycle = newestPredictDataDto.MinorCycle,
                    PredictData = newestPredictDataDto.PredictedData,
                    CurrentPredictPeriod = newestPredictDataDto.CurrentPredictPeriod,
                    PredictType = planInfo.DsType,
                    HistoryPredictResults = GetHistoryPredictResults(item.OrderByDescending(p => p.StartPeriod), item.Key, normConfig.LookupPeriodCount, planInfo.PlanNormTable),
                };
                var rightCount = planTrackNumber.HistoryPredictResults.Count(p => p == 0);
                var totleCount = planTrackNumber.HistoryPredictResults.Count(p => p != 2);
                var currentScore = Math.Round((double)rightCount / totleCount, 2);
                planTrackNumber.CurrentScore = currentScore;
                WritePlanTrackNumbers(item, planInfo, currentScore);
                planTrackNumbers.Add(planTrackNumber);
            });

            return planTrackNumbers.OrderBy(p => p.Sort).ToList();
        }

        #endregion private methods
    }
}