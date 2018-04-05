using System.Collections.Generic;
using System.Linq;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Collections;
using Lottery.Infrastructure.Enums;

namespace Lottery.Engine.ComputePredictResult
{
    public class NumComputePredictResult : BasePredictData
    {
        public NumComputePredictResult(IDictionary<int, double> predictedDataRate) : base(predictedDataRate)
        {
        }

        public override string GetPredictedData(PlanInfoDto normPlanInfo, NormConfigDto userNorm)
        {
            if (normPlanInfo.DsType == PredictType.Fix)
            {
                var predictedDataList = _predictedDataRate.OrderByDescending(p => p.Value).Select(p => p.Key).ToList();
                var result = predictedDataList.Take(userNorm.ForecastCount).ToString(",");
                return result;
            }
            else
            {
                var predictedDataList = _predictedDataRate.OrderBy(p => p.Value).Select(p => p.Key).ToList();
                var result = predictedDataList.Take(userNorm.ForecastCount).ToString(",");
                return result;
            }
        }
    }
}