using System;
using System.Collections.Generic;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Enums;

namespace Lottery.Engine.Predictor
{
    public class MockPredictor : BasePredictor
    {
        public MockPredictor(LotteryInfoDto lotteryInfo, AlgorithmType algorithmType) : base(lotteryInfo, algorithmType)
        {
        }


        public override IDictionary<int, double> Predictor(List<int> data, int count, int k, int historyCount, Tuple<int, int> valInfo)
        {
            var rdm = new Random();
            var result = new Dictionary<int,double>();
            for (int i = valInfo.Item1; i <= valInfo.Item2; i++)
            {
                int rVal = rdm.Next(0, 100);
                var precent = (double) rVal / 100;
                result.Add(i,precent);
            }
            return result;
        }
    }
}