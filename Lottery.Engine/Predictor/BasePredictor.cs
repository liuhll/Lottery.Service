using System;
using System.Collections.Generic;
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

        public IDictionary<int,double> Predictor(List<int> data, int count,int k)
        {
            var result = new DiscreteMarkov.DiscreteMarkov(data, count, k);

            return result.PredictValue1;
        }
    }
}