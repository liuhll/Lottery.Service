using System;
using System.Collections.Generic;
using System.Linq;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Extensions;

namespace Lottery.Engine.ComputePredictResult
{
    public class SizeComputePredictResult : BasePredictData
    {
        private const string bigVal = "大";
        private const string smallVal = "小";
        public SizeComputePredictResult(IDictionary<int, double> predictedDataRate) : base(predictedDataRate)
        {
        }


        protected override ICollection<string> GetPredictedDataList(PlanInfoDto normPlanInfo, NormConfigDto userNorm)
        {
            double bigPercent = 0;
            double smallPercent = 0;
            var perdictedVal = new List<string>();
            foreach (var item in _predictedDataRate)
            {
                if (_predictedDataRate.Count / 2 > item.Key)
                {
                    bigPercent += item.Value;
                }
                else
                {
                    smallPercent += item.Value;
                }
            }
            if (bigPercent > smallPercent)
            {
                perdictedVal.Add(bigVal);
                perdictedVal.Add(smallVal);
            }
            else
            {
                perdictedVal.Add(smallVal);
                perdictedVal.Add(bigVal);

            }
            return perdictedVal.ToList();
        }
    }
}