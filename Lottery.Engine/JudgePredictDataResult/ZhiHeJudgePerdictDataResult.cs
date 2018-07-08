using System.Linq;
using Lottery.Dtos.Lotteries;
using Lottery.Engine.LotteryData;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Extensions;

namespace Lottery.Engine.JudgePredictDataResult
{
    public class ZhiHeJudgePerdictDataResult : BaseJudgePerdictDataResult
    {
        private string[] zhiHeVal = new[] { "质", "合" };
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
            var numData = lotteryNumber[postion]; var sizeCriticalVal = planInfo.PositionInfos.First().MaxValue / 2;
            if (numData.IsPrime())
            {
                lotteryData = zhiHeVal[0];
            }
            else
            {
                lotteryData = zhiHeVal[1];
            }
            return lotteryData;
        }
    }
}