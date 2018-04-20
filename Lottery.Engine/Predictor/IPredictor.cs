using Lottery.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace Lottery.Engine.Predictor
{
    public interface IPerdictor
    {
        AlgorithmType AlgorithmType { get; }

        IDictionary<int, double> Predictor(List<int> data, int count, int k, int historyCount, Tuple<int, int> valInfo);
    }
}