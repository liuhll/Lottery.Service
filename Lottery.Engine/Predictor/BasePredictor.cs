using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace Lottery.Engine.Predictor
{
    public abstract class BasePredictor : IPerdictor
    {
        protected readonly LotteryInfoDto _LotteryInfo;

        protected BasePredictor(LotteryInfoDto lotteryInfo, AlgorithmType algorithmType)
        {
            _LotteryInfo = lotteryInfo;
            AlgorithmType = algorithmType;
        }

        public AlgorithmType AlgorithmType { get; }

        public abstract IDictionary<int, double> Predictor(List<int> data, int count, int k, int historyCount, Tuple<int, int> valInfo);
    }
}