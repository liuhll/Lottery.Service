using Lottery.Infrastructure;
using Lottery.Infrastructure.Exceptions;
using System.Collections.Generic;

namespace Lottery.Engine.ComputePredictResult
{
    public class ComputePredictFatory
    {
        public static IComputePredictResult CreateComputePredictResult(string predictCode, IDictionary<int, double> predictedDataRate)
        {
            IComputePredictResult predictResult;
            switch (predictCode)
            {
                case PredictCodeDefinition.NopNumCode:
                case PredictCodeDefinition.NumCode:
                case PredictCodeDefinition.JzNumMiCode:
                case PredictCodeDefinition.JzNumMxCode:
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
                case PredictCodeDefinition.ZhiHeCode:
                    predictResult = new ZhiHeComputePredictResult(predictedDataRate);
                    break;
                case PredictCodeDefinition.HeZhiCode:
                    predictResult = new HezhiComputePredictResult(predictedDataRate);
                    break;
                case PredictCodeDefinition.RxNumCode:
                    predictResult =new RxNumComputePredictResult(predictedDataRate);
                    break;
                //case PredictCodeDefinition.JzNumMiCode:
                //    predictResult = new JzNumMiComputePredictResult(predictedDataRate);
                //    break;
                //case PredictCodeDefinition.JzNumMxCode:
                //    predictResult =new JzNumMxComputePredictResult(predictedDataRate);
                //    break;
                case PredictCodeDefinition.ZuXuanCode:
                    predictResult =new ZuXuanComputePredictResult(predictedDataRate);
                    break;
                default:
                    throw new LotteryException("不存在该类型的数据结果计算器");
            }
            return predictResult;
        }
    }
}