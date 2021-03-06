﻿using System;
using Lottery.Dtos.Lotteries;
using System.Collections.Generic;
using System.Linq;
using Lottery.Infrastructure.Collections;

namespace Lottery.Engine.ComputePredictResult
{
    public class ShapeComputePredictResult : BasePredictData
    {
        private const string singleVal = "单";
        private const string doubleVal = "双";

        private string[] danShuangVal = new[] { "单", "双" };

        public ShapeComputePredictResult(IDictionary<int, double> predictedDataRate) : base(predictedDataRate)
        {
        }

        protected override ICollection<string> GetPredictedDataList(PlanInfoDto normPlanInfo, NormConfigDto userNorm)
        {
            //var perdictedVal = new List<string>();
            //double singlePercent = 0;
            //double doublePercent = 0;
            //foreach (var item in _predictedDataRate)
            //{
            //    if (item.Key % 2 == 0)
            //    {
            //        doublePercent += item.Value;
            //    }
            //    else
            //    {
            //        singlePercent += item.Value;
            //    }
            //}
            //if (singlePercent > doublePercent)
            //{
            //    perdictedVal.Add(singleVal);
            //    perdictedVal.Add(doubleVal);
            //}
            //else
            //{
            //    perdictedVal.Add(doubleVal);
            //    perdictedVal.Add(singleVal);
            //}
            var perdictedVal = danShuangVal.ToList().Shuffle().ToList();
            return perdictedVal;
        }
    }
}