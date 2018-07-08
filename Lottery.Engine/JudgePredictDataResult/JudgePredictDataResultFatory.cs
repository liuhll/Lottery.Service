using Lottery.Engine.ComputePredictResult;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Exceptions;

namespace Lottery.Engine.JudgePredictDataResult
{
    public class JudgePredictDataResultFatory
    {
        public static IJudgePredictDataResult CreateJudgePredictDataResult(string predictCode)
        {
            IJudgePredictDataResult result;
            switch (predictCode)
            {
                case PredictCodeDefinition.NumCode:
                    result = new NumberJudgePerdictDataResult();
                    break;
                case PredictCodeDefinition.NopNumCode:
                    result = new NopNumJudgePredictDataResult();
                    break;

                case PredictCodeDefinition.LhCode:
                    result = new LhJudgePerdictDataResult();
                    break;

                case PredictCodeDefinition.RankCode:
                    result = new RankJudgePerdictDataResult();
                    break;

                case PredictCodeDefinition.ShapeCode:
                    result = new ShapeJudgePerdictDataResult();
                    break;

                case PredictCodeDefinition.SizeCode:
                    result = new SizeJudgePerdictDataResult();
                    break;
                case PredictCodeDefinition.ZhiHeCode:
                    result = new ZhiHeJudgePerdictDataResult();
                    break;
                case PredictCodeDefinition.HeZhiCode:
                    result = new HeZhiJudgePerdictDataResult();
                    break;
                case PredictCodeDefinition.RxNumCode:
                    result = new RxNumJudgePredictDataResult();
                    break;
                case PredictCodeDefinition.JzNumMxCode:
                    result = new JzNumMxJudgePredictDataResult();
                    break;
                case PredictCodeDefinition.JzNumMiCode:
                    result = new JzNumMiJudgePredictDataResult();
                    break;
                case PredictCodeDefinition.ZuXuanCode:
                    result = new ZuXuanJudgePredictDataResult();
                    break;
                case PredictCodeDefinition.JunZhiCode:
                    result = new JunzhiJudgePredictResult();
                    break;
                default:
                    throw new LotteryException("不存在该类型的数据结果计算器");
            }
            return result;
        }
    }
}