using Lottery.Dtos.Lotteries;

namespace Lottery.Engine.ComputePredictResult
{
    public interface IComputePredictResult
    {
        // string GetPredictedData(PredictType dsType, int userNormForecastCount);
        string GetPredictedData(PlanInfoDto normPlanInfo, NormConfigDto userNorm);
    }
}