using Lottery.Dtos.Lotteries;

namespace Lottery.Engine.Predictor
{
    public class ShapePredictor : BasePredictor
    {
        public ShapePredictor(LotteryInfoDto lotteryInfo) : base(lotteryInfo)
        {
        }

        public override string PredictCode => "SHAPE";
    }
}