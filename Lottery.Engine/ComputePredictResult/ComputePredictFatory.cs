﻿using System.Collections.Generic;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Exceptions;

namespace Lottery.Engine.ComputePredictResult
{
    public class ComputePredictFatory
    {
        public static IComputePredictResult CreateComputePredictResult(string predictCode, IDictionary<int, double> predictedDataRate)
        {
            IComputePredictResult predictResult;
            switch (predictCode)
            {
                case PredictCodeDefinition.NumCode:
                    predictResult = new NumComputePredictResult(predictedDataRate);
                    break;
                case PredictCodeDefinition.LhCode:
                    predictResult = new LhComputePredictResult(predictedDataRate);
                    break;
                case PredictCodeDefinition.RankCode:
                    predictResult = new RankComputePredictResult(predictedDataRate);
                    break;
                case PredictCodeDefinition.ShapeCode:
                    predictResult = new ShapeComputePredictResult(predictedDataRate);
                    break;
                case PredictCodeDefinition.SizeCode:
                    predictResult = new SizeComputePredictResult(predictedDataRate);
                    break;
                default:
                    throw new LotteryException("不存在该类型的数据结果计算器");

            }
            return predictResult;
        }
    }
}