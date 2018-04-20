using ECommon.Components;
using ECommon.Extensions;
using Lottery.AppService.LotteryData;
using Lottery.Dtos.Lotteries;
using Lottery.QueryServices.Lotteries;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.AppService.Plan
{
    [Component]
    public class PlanTrackAppService : IPlanTrackAppService
    {
        private readonly ILotteryPredictDataQueryService _lotteryPredictDataQueryService;
        private readonly IPlanInfoQueryService _planInfoQueryService;
        private readonly ILotteryDataAppService _lotteryDataAppService;

        public PlanTrackAppService(ILotteryPredictDataQueryService lotteryPredictDataQueryService,
            IPlanInfoQueryService planInfoQueryService,
            ILotteryDataAppService lotteryDataAppService)
        {
            _lotteryPredictDataQueryService = lotteryPredictDataQueryService;
            _planInfoQueryService = planInfoQueryService;
            _lotteryDataAppService = lotteryDataAppService;
        }

        public PlanTrackDetail GetPlanTrackDetail(NormConfigDto userNorm, string lotteryCode, string userId)
        {
            var planInfo = _planInfoQueryService.GetPlanInfoById(userNorm.PlanId);
            var prodictDatas = _lotteryPredictDataQueryService.GetNormPredictDatas(userNorm.Id, planInfo.PlanNormTable, userNorm.LookupPeriodCount + 1, lotteryCode);
            var currentPredictData = AutoMapper.Mapper.Map<PredictDataDetail>(prodictDatas.FirstOrDefault(p => p.PredictedResult == 2));
            currentPredictData.PredictType = planInfo.DsType;
            // currentPredictData.LotteryData = _lotteryDataAppService.GetLotteryData(planInfo.LotteryInfo.Id,currentPredictData.CurrentPredictPeriod).Data;
            var historyPredictDatas = AutoMapper.Mapper.Map<ICollection<PredictDataDetail>>(prodictDatas.Where(p => p.PredictedResult != 2).ToList());
            historyPredictDatas.ForEach(item =>
            {
                item.LotteryData = _lotteryDataAppService.GetLotteryData(planInfo.LotteryInfo.Id, item.CurrentPredictPeriod).Data;
                item.PredictType = planInfo.DsType;
            });
            var planTrackDetail = new PlanTrackDetail()
            {
                CurrentPredictData = currentPredictData,
                FinalLotteryData = _lotteryDataAppService.GetFinalLotteryData(planInfo.LotteryInfo.Id),
                HistoryPredictDatas = historyPredictDatas,
                NormId = userNorm.Id,
                PlanId = planInfo.Id,
                PlanName = planInfo.PlanName,
                Sort = userNorm.Sort,
                StatisticData = ComputeStatisticData(currentPredictData, historyPredictDatas, userNorm)
            };
            return planTrackDetail;
        }

        private StatisticData ComputeStatisticData(PredictDataDetail currentPredictData, ICollection<PredictDataDetail> historyPredictDatas, NormConfigDto userNorm)
        {
            if (currentPredictData == null)
            {
                return null;
            }
            var statisticData = new StatisticData()
            {
                CurrentScore = currentPredictData.CurrentScore,
            };
            statisticData.MaxSerieError =
                ComputeArrayMaxCount(historyPredictDatas.Select(p => p.PredictedResult).ToArray(), 1);
            statisticData.MaxSerieRight =
                ComputeArrayMaxCount(historyPredictDatas.Select(p => p.PredictedResult).ToArray(), 0);

            int currentSerie = 0;
            var currentResult = historyPredictDatas.First().PredictedResult;
            foreach (var data in historyPredictDatas)
            {
                currentSerie++;
                if (currentResult != data.PredictedResult)
                {
                    break;
                }
            }
            statisticData.CurrentSerie = currentSerie;
            var minorCycleStatistic = new Dictionary<int, int>();
            for (int i = 1; i <= userNorm.PlanCycle; i++)
            {
                var thisCysleRightCount = historyPredictDatas.Count(p => p.PredictedResult == 0 && p.MinorCycle == i);
                minorCycleStatistic.Add(i, thisCysleRightCount);
            }
            statisticData.MinorCycleStatistic = minorCycleStatistic;
            return statisticData;
        }

        private int ComputeArrayMaxCount(int[] array, int val)
        {
            var maxSerie = 0;
            var maxSerieCount = 0;
            foreach (var currentNum in array)
            {
                if (currentNum != val)
                {
                    maxSerieCount = 0;
                }
                else
                {
                    maxSerieCount++;
                }
                if (maxSerieCount > maxSerie)
                {
                    maxSerie = maxSerieCount;
                }
            }
            return maxSerie;
        }
    }
}