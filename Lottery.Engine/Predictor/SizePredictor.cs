using Lottery.Dtos.Lotteries;

namespace Lottery.Engine.Predictor
{
    public class SizePerdictor : BasePredictor
    {
        public SizePerdictor(LotteryInfoDto lotteryInfo) : base(lotteryInfo)
        {
        }

        public override string PredictCode => "SIZE";
    }
}