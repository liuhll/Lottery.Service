using System.Collections.Generic;

namespace Lottery.Engine.Predictor
{
    public interface IPerdictor
    {
        string PredictCode { get; }

        IDictionary<int, double> Predictor(List<int> data, int count,int k);
    }
}