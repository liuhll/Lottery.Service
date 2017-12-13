using System.Collections.Generic;
using ECommon.Components;
using Lottery.Dtos.Lotteries;
using Lottery.QueryServices.Lotteries;

namespace Lottery.AppService.Predict
{
    [Component]
    public class LotteryPredictDataService : ILotteryPredictDataService
    {
        private readonly ILotteryFinalDataQueryService _lotteryFinalDataQueryService;
        private readonly ILotteryQueryService _lotteryQueryService;
        private readonly ILotteryPredictDataQueryService _lotteryPredictDataQueryService;
        private readonly IPlanInfoQueryService _planInfoQueryService;

        public LotteryPredictDataService(ILotteryFinalDataQueryService lotteryFinalDataQueryService,
            ILotteryQueryService lotteryQueryService,
            ILotteryPredictDataQueryService lotteryPredictDataQueryService, 
            IPlanInfoQueryService planInfoQueryService)
        {
            _lotteryFinalDataQueryService = lotteryFinalDataQueryService;
            _lotteryQueryService = lotteryQueryService;
            _lotteryPredictDataQueryService = lotteryPredictDataQueryService;
            _planInfoQueryService = planInfoQueryService;
        }

        public ICollection<PredictDataDto> PredictNormData(string lotteryId, NormConfigDto userNorm,int predictPeroid)
        {
            var lastPredictPeriod = GetLastPredictNormPeriod(lotteryId, userNorm);


            return null;
        }

        #region private Methods

        private int GetLastPredictNormPeriod(string lotteryId, NormConfigDto userNorm)
        {
            var normPlanInfo = _planInfoQueryService.GetPlanInfoById(userNorm.PlanId);
            var lastPredictData = _lotteryPredictDataQueryService.GetLastPredictData(userNorm.Id,normPlanInfo.PlanNormTable);
            var lastLotteryData = _lotteryFinalDataQueryService.GetFinalData(lotteryId);
            var predictCount = userNorm.PlanCycle * userNorm.LookupPeriodCount;
            var theoryStartPredictPreoid = lastLotteryData.FinalPeriod - predictCount;
            if (lastPredictData != null)
            {
                var userNormPredictPeroid = lastPredictData.StartPeriod + lastPredictData.MinorCycle - 1;
                if (userNormPredictPeroid > theoryStartPredictPreoid)
                {
                    return userNormPredictPeroid;
                }
            }

            return theoryStartPredictPreoid;
        }

        #endregion
    }
}