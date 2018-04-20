using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace Lottery.Engine.Predictor
{
    public class StochasticPredictor : BasePredictor
    {
        public StochasticPredictor(LotteryInfoDto lotteryInfo, AlgorithmType algorithmType) : base(lotteryInfo, algorithmType)
        {
        }

        public override IDictionary<int, double> Predictor(List<int> data, int count, int k, int historyCount, Tuple<int, int> valInfo)
        {
            throw new NotImplementedException();
        }
    }
}