using System;
using System.Collections.Generic;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Enums;

namespace Lottery.Engine.Predictor
{
    public class DiscreteMarkovPredictor : BasePredictor
    {
        public DiscreteMarkovPredictor(LotteryInfoDto lotteryInfo, AlgorithmType algorithmType) : base(lotteryInfo, algorithmType)
        {
        }

        public override IDictionary<int, double> Predictor(List<int> data, int count, int k, int historyCount, Tuple<int, int> valInfo)
        {
            var result = new DiscreteMarkov.DiscreteMarkov(data, count, k);
            return result.PredictValue1;
        }

      
    }
}