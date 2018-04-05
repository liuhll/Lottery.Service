using System.Collections.Generic;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Enums;

namespace Lottery.Engine.ComputePredictResult
{
    public abstract class BasePredictData : IComputePredictResult
    {
        protected readonly IDictionary<int, double> _predictedDataRate;

        protected BasePredictData(IDictionary<int, double> predictedDataRate)
        {
            _predictedDataRate = predictedDataRate;
        }


        public abstract string GetPredictedData(PlanInfoDto normPlanInfo, NormConfigDto userNorm);
    }
}