using System.Collections.Generic;
using System.Linq;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Enums;

namespace Lottery.Engine.ComputePredictResult
{
    public class NoNumComputePredictResult : BasePredictData
    {
        public NoNumComputePredictResult(IDictionary<int, double> predictedDataRate) : base(predictedDataRate)
        {
        }

        protected override ICollection<string> GetPredictedDataList(PlanInfoDto normPlanInfo, NormConfigDto userNorm)
        {
            if (normPlanInfo.DsType == PredictType.Fix)
            {
                var predictedDataList = _predictedDataRate.OrderByDescending(p => p.Value).Select(p => p.Key.ToString()).ToList();
                var result = predictedDataList.ToList();
                return result;
            }
            else
            {
                var predictedDataList = _predictedDataRate.OrderBy(p => p.Value).Select(p => p.Key.ToString()).ToList();
                var result = predictedDataList;
                return result;
            }
        }
    }
}