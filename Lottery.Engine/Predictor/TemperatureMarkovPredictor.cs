using System;
using System.Collections.Generic;
using System.Linq;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Enums;

namespace Lottery.Engine.Predictor
{
    public class TemperatureMarkovPredictor : BasePredictor
    {
        public TemperatureMarkovPredictor(LotteryInfoDto lotteryInfo, AlgorithmType algorithmType) : base(lotteryInfo, algorithmType)
        {
        }


        public override IDictionary<int, double> Predictor(List<int> data, int count, int k, int historyCount, Tuple<int, int> valInfo)
        {
            var result = new Dictionary<int, double>();
            var totalCount = data.Count;
            for (int i = valInfo.Item1; i <= valInfo.Item2; i++)
            {
                var valCount = data.Count(p=> p == i);
                var percent = (double) valCount / totalCount;
                result.Add(i,percent);
            }
            return result;
        }
    }
}