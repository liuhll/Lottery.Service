using System;
using System.Linq;
using Lottery.Dtos.Lotteries;
using Lottery.Engine.LotteryData;
using Lottery.Infrastructure.Enums;

namespace Lottery.Engine.JudgePredictDataResult
{
    public class LhJudgePerdictDataResult : BaseJudgePerdictDataResult
    {
        private string[] longhuVal = new[] { "龙", "虎" };
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

            var postion = planInfo.PositionInfos.First().Position;
            var lotteryNumberData = GetLotteryNumberData(lotteryNumber, postion, planInfo);
            bool isRight;
            var lhPredictData = startPeriodData.PredictedData.Split(',').Select(p => p.ToString());
            if (planInfo.DsType == PredictType.Fix)
            {
                isRight = lhPredictData.Contains(lotteryNumberData);
            }
            else
            {
                isRight = !lhPredictData.Contains(lotteryNumberData);
            }
            if (isRight)
            {
                return PredictedResult.Right;
            }
            else
            {
                if (startPeriodData.CurrentPredictPeriod >= startPeriodData.EndPeriod)
                {
                    return PredictedResult.Error;
                }
                return PredictedResult.Running;
            }

        }

        protected override object GetLotteryNumberData(LotteryNumber lotteryNumber, int postion, PlanInfoDto planInfo)
        {
            string lotteryData = string.Empty;
            var firstVal = lotteryNumber[postion];
            var secondVal = lotteryNumber[lotteryNumber.Datas.Length - postion + 1];
            if (firstVal > secondVal)
            {
                lotteryData = longhuVal[0];
            }
            else
            {
                lotteryData = longhuVal[1];
            }
            return lotteryData;
        }
    }
}