using Lottery.Dtos.Lotteries;

namespace Lottery.Engine.Predictor
{
    public class RankPredictor : BasePredictor
    {
        public RankPredictor(LotteryInfoDto lotteryInfo) : base(lotteryInfo)
        {
        }

        public override string PredictCode => "RANK";
    }
}