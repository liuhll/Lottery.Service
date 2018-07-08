using System;
using System.Collections.Generic;
using System.Linq;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Collections;

namespace Lottery.Engine.ComputePredictResult
{
    public class JunzhiComputePredictResult : BasePredictData
    {
        public JunzhiComputePredictResult(IDictionary<int, double> predictedDataRate) : base(predictedDataRate)
        {
        }

        protected override ICollection<string> GetPredictedDataList(PlanInfoDto normPlanInfo, NormConfigDto userNorm)
        {

            var minVal = _predictedDataRate.Keys.Min();
            var maxVal = _predictedDataRate.Keys.Max();

            var rd = new Random();

            var result = new List<string>();

            while (result.Count < userNorm.ForecastCount)
            {
                result.AddIfNotContains(rd.Next(minVal, maxVal).ToString());
            }
            return result;
        }
    }
}