using Lottery.Dtos.Lotteries;

namespace Lottery.Engine.Predictor
{
    public abstract class BasePredictor : IPerdictor
    {
        protected readonly LotteryInfoDto _LotteryInfo;

        protected BasePredictor(LotteryInfoDto lotteryInfo)
        {
            _LotteryInfo = lotteryInfo;


        }


        public abstract string PredictCode { get; }
    }
}