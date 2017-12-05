using Lottery.Dtos.Lotteries;

namespace Lottery.Engine.Predictor
{
    public class LhPredictor : BasePredictor
    {
        public LhPredictor(LotteryInfoDto lotteryInfo) : base(lotteryInfo)
        {
            
        }

        public override string PredictCode => "LH";
    }
}