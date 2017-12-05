using Lottery.Dtos.Lotteries;

namespace Lottery.Engine.Predictor
{
    public class NumPerdictor : BasePredictor
    {
        public NumPerdictor(LotteryInfoDto lotteryInfo) : base(lotteryInfo)
        {
        }

        public override string PredictCode => "NUM";
    }
}