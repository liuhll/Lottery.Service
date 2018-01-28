using System;
using System.Collections.Generic;
using System.Linq;
using ECommon.Components;
using ECommon.Extensions;
using Lottery.AppService.Predict;
using Lottery.Dtos.Lotteries;
using Lottery.Engine.LotteryData;
using Lottery.Engine.TimeRule;
using Lottery.Infrastructure.Exceptions;
using Lottery.QueryServices.Lotteries;

namespace Lottery.AppService.LotteryData
{
    [Component]
    public class LotteryDataAppService : ILotteryDataAppService
    {
        private readonly ILotteryDataQueryService _lotteryDataQueryService;       
        private readonly INormConfigQueryService _normConfigQueryService;
        private readonly ILotteryPredictDataService _lotteryPredictDataService;
        private readonly ILotteryFinalDataQueryService _lotteryFinalDataQueryService;
        private readonly ILotteryQueryService _lotteryQueryService;

        public LotteryDataAppService(
            ILotteryDataQueryService lotteryDataQueryService,
            INormConfigQueryService normConfigQueryService, 
            ILotteryPredictDataService lotteryPredictDataService,
            ILotteryFinalDataQueryService lotteryFinalDataQueryService,
            ILotteryQueryService lotteryQueryService)
        {
            _lotteryDataQueryService = lotteryDataQueryService;
            _normConfigQueryService = normConfigQueryService;
            _lotteryPredictDataService = lotteryPredictDataService;
            _lotteryFinalDataQueryService = lotteryFinalDataQueryService;
            _lotteryQueryService = lotteryQueryService;
        }

        public ICollection<LotteryDataDto> AllDatas(string lotteryId)
        {
            return _lotteryDataQueryService.GetAllDatas(lotteryId);
        }

        public ILotteryDataList LotteryDataList(string lotteryId)
        {
            var datas = _lotteryDataQueryService.GetAllDatas(lotteryId);

            return new LotteryDataList(datas);
        }

        public IList<PredictDataDto> NewLotteryDataList(string lotteryId, int? predictPeroid, string userId)
        {
            //  var lotteryInfo = _lotteryQueryService.GetLotteryInfoByCode(lotteryId);
            var finalLotteryData = _lotteryFinalDataQueryService.GetFinalData(lotteryId);

            if (!predictPeroid.HasValue)
            {
                predictPeroid = finalLotteryData.FinalPeriod + 1;
            }

            if (finalLotteryData.FinalPeriod >= predictPeroid)
            {
                throw new LotteryDataException($"预测的期数第{predictPeroid}期必须大于最后的开奖期数{finalLotteryData.FinalPeriod}");
            }

            var predictDatas = new List<PredictDataDto>();
            var userNorms = _normConfigQueryService.GetUserOrDefaultNormConfigs(lotteryId, userId);
            foreach (var userNorm in userNorms)
            {
                predictDatas.AddRange(PredictNormData(lotteryId, userNorm, predictPeroid.Value));
            }
            return predictDatas;
        }

        public ICollection<LotteryDataDto> GetList(string lotteryId)
        {
            var datas = _lotteryDataQueryService.GetAllDatas(lotteryId);
            return datas;

        }

        public FinalLotteryDataOutput GetFinalLotteryData(string lotteryId)
        {
            var lotteryInfo = _lotteryQueryService.GetLotteryInfoById(lotteryId);
            var finalData = _lotteryFinalDataQueryService.GetFinalData(lotteryId);
           // var todayActLotteryCount = finalData.FinalPeriod - finalData.TodayFirstPeriod + 1;
            var lotteryTimerManager = new TimeRuleManager(lotteryInfo);

            var finalLotteryDataOutput = new FinalLotteryDataOutput()
            {
                Period = finalData.FinalPeriod,
                Data = finalData.Data,
                LotteryTime = finalData.LotteryTime,
              //  NextLotteryTime = lotteryTimerManager.NextLotteryTime().Value,
                NextPeriod = finalData.FinalPeriod + 1,
            };
         
            if (lotteryTimerManager.FinalPeriodIsLottery(finalData))
            {
                finalLotteryDataOutput.IsLotteryData = true;
                DateTime nextLotteryTime;
                if (lotteryTimerManager.ParseNextLotteryTime(out nextLotteryTime))
                {
                    finalLotteryDataOutput.NextLotteryTime = nextLotteryTime;
                    var intervalTime = nextLotteryTime - DateTime.Now;
                    finalLotteryDataOutput.RemainSeconds = (int)intervalTime.TotalSeconds;
                }

            }
            else
            {
                finalLotteryDataOutput.IsLotteryData = false;
                finalLotteryDataOutput.RemainSeconds = 0;
            }
            return finalLotteryDataOutput;

        }

        public ICollection<PlanTrackNumber> GetpredictResultData(IList<PredictDataDto> data,string lotteryId)
        {
            var planTrackNumbers = new List<PlanTrackNumber>();
            data.GroupBy(p => p.NormConfigId).ForEach(item =>
            {
                var planInfo = _normConfigQueryService.GetNormPlanInfoByNormId(item.Key,lotteryId);
                var newestLotteryInfo = item.OrderByDescending(p => p.StartPeriod).First();
                var planTrackNumber = new PlanTrackNumber()
                {
                    PlanId = planInfo.Id,
                    PlanName = planInfo.PlanName,
                    EndPeriod = newestLotteryInfo.EndPeriod,
                    StartPeriod = newestLotteryInfo.StartPeriod,
                    MinorCycle = newestLotteryInfo.MinorCycle,
                    PredictData = newestLotteryInfo.PredictedData,
                    PredictType = planInfo.DsType,
                    HistoryPredictResults = GetHistoryPredictResults(item.OrderByDescending(p=>p.StartPeriod)),
                };
                planTrackNumbers.Add(planTrackNumber);
            });
      
            return planTrackNumbers;
        }

        private int[] GetHistoryPredictResults(IOrderedEnumerable<PredictDataDto> predictDatas)
        {
            var historyPredictResults = new List<int>();
            foreach (var item in predictDatas)
            {
                historyPredictResults.Add((int)item.PredictedResult);
            }
            return historyPredictResults.ToArray();
        }


        #region 私有方法
        private IEnumerable<PredictDataDto> PredictNormData(string lotteryId, NormConfigDto userNorm, int predictPeroid)
        {
            return _lotteryPredictDataService.PredictNormData(lotteryId, userNorm, predictPeroid);
        }


        #endregion


    }
}