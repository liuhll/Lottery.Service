using System;
using Lottery.Dtos.Lotteries;
using System.Collections.Generic;
using System.Linq;
using Lottery.Infrastructure.Collections;

namespace Lottery.Engine.ComputePredictResult
{
    public class SizeComputePredictResult : BasePredictData
    {
        private const string bigVal = "大";
        private const string smallVal = "小";
        private string[] sizeVal = new[] { "大", "小" };
        public SizeComputePredictResult(IDictionary<int, double> predictedDataRate) : base(predictedDataRate)
        {
        }

        protected override ICollection<string> GetPredictedDataList(PlanInfoDto normPlanInfo, NormConfigDto userNorm)
        {
            //double bigPercent = 0;
            //double smallPercent = 0;
            // var perdictedVal = new List<string>();
            //foreach (var item in _predictedDataRate)
            //{
            //    if (_predictedDataRate.Count / 2 > item.Key)
            //    {
            //        bigPercent += item.Value;
            //    }
            //    else
            //    {
            //        smallPercent += item.Value;
            //    }
            //}
            //if (bigPercent > smallPercent)
            //{
            //    perdictedVal.Add(bigVal);
            //    perdictedVal.Add(smallVal);
            //}
            //else
            //{
            //    perdictedVal.Add(smallVal);
            //    perdictedVal.Add(bigVal);
            //}

            var perdictedVal = sizeVal.ToList().Shuffle().ToList();
            return perdictedVal;
        }
    }
}