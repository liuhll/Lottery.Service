using System;
using System.Linq;
using Lottery.Dtos.Lotteries;
using Lottery.Engine.LotteryData;
using Lottery.Infrastructure.Enums;

namespace Lottery.Engine.JudgePredictDataResult
{
    public class ShapeJudgePerdictDataResult : BaseJudgePerdictDataResult
    {
        private string[] danShuangVal = new[] { "单", "双" };

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
            var lotteryNumberData = GetLotteryNumberData(lotteryNumber, postion, planInfo).ToString();
            bool isRight;
            var numPredictData = startPeriodData.PredictedData.Split(',').Select(p => p.ToString());

            if (planInfo.DsType == PredictType.Fix)
            {
                isRight = numPredictData.Contains(lotteryNumberData);
            }
            else
            {
                isRight = !numPredictData.Contains(lotteryNumberData);
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
            var lotteryData = string.Empty;
            var numData = lotteryNumber[postion];
            if (numData % 2 == 0)
            {
                lotteryData = danShuangVal[1];
            }
            else
            {
                lotteryData = danShuangVal[0];
            }
            return lotteryData;
        }
    }
}