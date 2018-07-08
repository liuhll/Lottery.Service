using System;
using System.Collections.Generic;
using System.Linq;
using Lottery.Dtos.Lotteries;
using Lottery.Engine.LotteryData;
using Lottery.Infrastructure.Enums;

namespace Lottery.Engine.JudgePredictDataResult
{
    public class NopNumJudgePredictDataResult : BaseJudgePerdictDataResult
    {
        public override PredictedResult JudgePredictDataResult(LotteryInfoDto lotteryInfo, PredictDataDto startPeriodData,
            NormConfigDto userNormConfig)
        {
            var planInfo = _planInfoQueryService.GetPlanInfoById(userNormConfig.PlanId);
            var lotteryData = _lotteryDataQueryService.GetPredictPeriodData(lotteryInfo.Id, startPeriodData.CurrentPredictPeriod);
            if (lotteryData == null)
            {
                return PredictedResult.Running;
            }
            var lotteryNumber = new LotteryNumber(lotteryData);
            var lotteryNumbers = new List<int>();
            foreach (var position in planInfo.PositionInfos)
            {
                var lotteryNumberData = GetLotteryNumberData(lotteryNumber, position.Position, planInfo);
                lotteryNumbers.Add(Convert.ToInt32(lotteryNumberData));
            }
            bool isRight;
            var predictNumber = startPeriodData.PredictedData.Split(',').Select(p => Convert.ToInt32(p)).ToArray();
            if (planInfo.DsType == PredictType.Fix)
            {
                if (lotteryNumbers.All(p => predictNumber.Contains(p)))
                {
                    return PredictedResult.Right;
                }
                else if (startPeriodData.CurrentPredictPeriod >= startPeriodData.EndPeriod)
                {
                    return PredictedResult.Error;
                }
            }
            else
            {
                if (!lotteryNumbers.Any(p => predictNumber.Contains(p)))
                {
                    return PredictedResult.Right;
                }
                else if (startPeriodData.CurrentPredictPeriod >= startPeriodData.EndPeriod)
                {
                    return PredictedResult.Error;
                }
            }
            return PredictedResult.Running;

        }

        protected override object GetLotteryNumberData(LotteryNumber lotteryNumber, int postion, PlanInfoDto planInfo)
        {
            string lotteryData = string.Empty;
            lotteryData = lotteryNumber[postion].ToString();
            return lotteryData;
        }
    }
}