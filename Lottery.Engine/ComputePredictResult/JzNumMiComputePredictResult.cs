using System.Collections.Generic;
using Lottery.Dtos.Lotteries;

namespace Lottery.Engine.ComputePredictResult
{
    public class JzNumMiComputePredictResult : BasePredictData
    {
        public JzNumMiComputePredictResult(IDictionary<int, double> predictedDataRate) : base(predictedDataRate)
        {
        }

        protected override ICollection<string> GetPredictedDataList(PlanInfoDto normPlanInfo, NormConfigDto userNorm)
        {
            throw new System.NotImplementedException();
        }
    }
}