using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ECommon.Components;
using ECommon.Extensions;
using Lottery.AppService.Predict;
using Lottery.Dtos.Lotteries;
using Lottery.Engine.LotteryData;
using Lottery.Engine.TimeRule;
using Lottery.Infrastructure.Exceptions;
using Lottery.QueryServices.Lotteries;
using Lottery.QueryServices.Predicts;

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
        private readonly IPlanInfoQueryService _planInfoQueryService;
        private readonly IPredictService _predictService;

        public LotteryDataAppService(
            ILotteryDataQueryService lotteryDataQueryService,
            INormConfigQueryService normConfigQueryService, 
            ILotteryPredictDataService lotteryPredictDataService,
            ILotteryFinalDataQueryService lotteryFinalDataQueryService,
            ILotteryQueryService lotteryQueryService,
            IPlanInfoQueryService planInfoQueryService,
            IPredictService predictService)
        {
            _lotteryDataQueryService = lotteryDataQueryService;
            _normConfigQueryService = normConfigQueryService;
            _lotteryPredictDataService = lotteryPredictDataService;
            _lotteryFinalDataQueryService = lotteryFinalDataQueryService;
            _lotteryQueryService = lotteryQueryService;
            _planInfoQueryService = planInfoQueryService;
            _predictService = predictService;
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

        public IList<PredictDataDto> NewLotteryDataList(string lotteryId, string userId)
        {
            var lotteryInfo = _lotteryQueryService.GetLotteryInfoByCode(lotteryId);
            var finalLotteryData = _lotteryFinalDataQueryService.GetFinalData(lotteryId);
            var predictPeroid = finalLotteryData.FinalPeriod + 1;

            var predictDatas = new List<PredictDataDto>();
            var userNorms = _normConfigQueryService.GetUserOrDefaultNormConfigs(lotteryId, userId);
            foreach (var userNorm in userNorms)
            {
                predictDatas.AddRange(PredictNormData(lotteryInfo, userNorm, predictPeroid));
            }
            return predictDatas;
        }

        public IList<PredictDataDto> UpdateLotteryDataList(string lotteryId, string userId)
        {
            var lotteryInfo = _lotteryQueryService.GetLotteryInfoByCode(lotteryId);
            var finalLotteryData = _lotteryFinalDataQueryService.GetFinalData(lotteryId);
            var predictPeroid = finalLotteryData.FinalPeriod + 1;
            var predictDatas = new List<PredictDataDto>();
            var random = new Random(unchecked((int)DateTime.Now.Ticks));
            var userNorms = _normConfigQueryService.GetUserOrDefaultNormConfigs(lotteryId, userId);
            foreach (var userNorm in userNorms)
            {
                var planInfo = _planInfoQueryService.GetPlanInfoById(userNorm.PlanId);
                _predictService.DeleteHistoryPredictDatas(planInfo.LotteryInfo.LotteryCode, planInfo.PlanNormTable,userNorm.LookupPeriodCount,userNorm.PlanCycle);
                Thread.Sleep(200);
                userNorm.HistoryCount = random.Next(1, 10) * userNorm.HistoryCount;
                userNorm.UnitHistoryCount = random.Next(1, 10) * userNorm.UnitHistoryCount;
                predictDatas.AddRange(PredictNormData(lotteryInfo, userNorm, predictPeroid));
            }
            return predictDatas;
        }

        public IList<PredictDataDto> UpdateLotteryDataList(string lotteryId, string userId, string normId)
        {
            var lotteryInfo = _lotteryQueryService.GetLotteryInfoByCode(lotteryId);
            var finalLotteryData = _lotteryFinalDataQueryService.GetFinalData(lotteryId);
            var predictPeroid = finalLotteryData.FinalPeriod + 1;
            var predictDatas = new List<PredictDataDto>();
            
            var userNorm = _normConfigQueryService.GetUserNormConfig(normId);
            var planInfo = _planInfoQueryService.GetPlanInfoById(userNorm.PlanId);
            _predictService.DeleteHistoryPredictDatas(planInfo.LotteryInfo.LotteryCode, planInfo.PlanNormTable, userNorm.LookupPeriodCount, userNorm.PlanCycle);
            Thread.Sleep(200);
            predictDatas.AddRange(PredictNormData(lotteryInfo, userNorm, predictPeroid));
            return predictDatas;
        }

        public ICollection<LotteryDataDto> GetList(string lotteryId, DateTime? lotteryTime)
        {
            ICollection<LotteryDataDto> datas = null;
            if (lotteryTime == null)
            {
                datas = _lotteryDataQueryService.GetAllDatas(lotteryId);
            }
            else
            {
                datas = _lotteryDataQueryService.GetLotteryDatas(lotteryId, lotteryTime.Value);
            }
           
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

        public LotteryDataDto GetLotteryData(string lotteryInfoId, int currentPredictPeriod)
        {
            return GetList(lotteryInfoId,null).First(p => p.Period == currentPredictPeriod);
        }


        #region 私有方法
        private IEnumerable<PredictDataDto> PredictNormData(LotteryInfoDto lotteryInfo, NormConfigDto userNorm, int predictPeroid, bool isSwitchFormula = false)
        {
            return _lotteryPredictDataService.PredictNormData(lotteryInfo.Id, userNorm, predictPeroid,lotteryInfo.LotteryCode, isSwitchFormula);
        }


        #endregion


    }
}