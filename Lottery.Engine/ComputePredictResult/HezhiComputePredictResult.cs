using System;
using System.Collections.Generic;
using System.Linq;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Collections;
using Lottery.Infrastructure.Enums;

namespace Lottery.Engine.ComputePredictResult
{
    public class HezhiComputePredictResult : BasePredictData
    {
        public HezhiComputePredictResult(IDictionary<int, double> predictedDataRate) : base(predictedDataRate)
        {
        }

        protected override ICollection<string> GetPredictedDataList(PlanInfoDto normPlanInfo, NormConfigDto userNorm)
        {
            var count = normPlanInfo.PositionInfos.Count;

            var minVal = _predictedDataRate.Keys.Min() * count;
            var maxVal = _predictedDataRate.Keys.Max() * count;

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