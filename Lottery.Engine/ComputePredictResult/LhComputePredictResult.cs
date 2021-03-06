﻿using System;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.Engine.ComputePredictResult
{
    public class LhComputePredictResult : BasePredictData
    {
        private const string longVal = "龙";
        private const string huVal = "虎";

        private string[] longhuVal = new[] {"龙", "虎"};

        public LhComputePredictResult(IDictionary<int, double> predictedDataRate) : base(predictedDataRate)
        {
        }

        protected override ICollection<string> GetPredictedDataList(PlanInfoDto normPlanInfo, NormConfigDto userNorm)
        {
            var positionInfo = normPlanInfo.PositionInfos.First();

            var valArr = PredictVals(positionInfo.Position, normPlanInfo.DsType);
            return valArr.ToList();
        }

        private List<string> PredictVals(int position, PredictType predictType, int maxPosition = 10)
        {
            var valArr = new List<string>();
            //var longPosition = position;
            //var huPosition = maxPosition - position + 1;

            //if (predictType == PredictType.Fix)
            //{
            //    if (_predictedDataRate[longPosition] > _predictedDataRate[huPosition])
            //    {
            //        valArr.Add(longVal);
            //        valArr.Add(huVal);
            //    }
            //    else
            //    {
            //        valArr.Add(huVal);
            //        valArr.Add(longVal);
            //    }
            //}
            //else
            //{
            //    if (_predictedDataRate[longPosition] > _predictedDataRate[huPosition])
            //    {
            //        valArr.Add(huVal);
            //        valArr.Add(longVal);
            //    }
            //    else
            //    {
            //        valArr.Add(longVal);
            //        valArr.Add(huVal);
            //    }
            //}
            valArr = longhuVal.OrderBy(p => new Random().Next()).ToList();

            return valArr;
        }
    }
}