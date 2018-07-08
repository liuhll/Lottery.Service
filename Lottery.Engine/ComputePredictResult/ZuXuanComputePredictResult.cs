using System.Collections.Generic;
using Lottery.Dtos.Lotteries;

namespace Lottery.Engine.ComputePredictResult
{
    public class ZuXuanComputePredictResult : BasePredictData
    {
        public ZuXuanComputePredictResult(IDictionary<int, double> predictedDataRate) : base(predictedDataRate)
        {
        }

        protected override ICollection<string> GetPredictedDataList(PlanInfoDto normPlanInfo, NormConfigDto userNorm)
        {
            throw new System.NotImplementedException();
        }
    }
}