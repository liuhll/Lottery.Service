using System.Collections.Generic;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Enums;

namespace Lottery.Engine.ComputePredictResult
{
    public interface IComputePredictResult
    {
       // string GetPredictedData(PredictType dsType, int userNormForecastCount);
        string GetPredictedData(PlanInfoDto normPlanInfo, NormConfigDto userNorm);
    }
}