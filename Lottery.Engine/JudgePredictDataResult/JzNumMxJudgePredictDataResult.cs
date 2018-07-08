using Lottery.Dtos.Lotteries;
using Lottery.Engine.LotteryData;
using Lottery.Infrastructure.Enums;

namespace Lottery.Engine.JudgePredictDataResult
{
    public class JzNumMxJudgePredictDataResult : BaseJudgePerdictDataResult
    {
        public override PredictedResult JudgePredictDataResult(LotteryInfoDto lotteryInfo, PredictDataDto startPeriodData,
            NormConfigDto userNormConfig)
        {
            throw new System.NotImplementedException();
        }

        protected override object GetLotteryNumberData(LotteryNumber lotteryNumber, int postion, PlanInfoDto planInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}