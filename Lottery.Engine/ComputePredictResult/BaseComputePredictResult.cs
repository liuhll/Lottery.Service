using System;
using System.Collections.Generic;
using System.Linq;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Extensions;

namespace Lottery.Engine.ComputePredictResult
{
    public abstract class BasePredictData : IComputePredictResult
    {
        protected readonly IDictionary<int, double> _predictedDataRate;

        protected BasePredictData(IDictionary<int, double> predictedDataRate)
        {
            _predictedDataRate = predictedDataRate;
        }

        protected abstract ICollection<string> GetPredictedDataList(PlanInfoDto normPlanInfo, NormConfigDto userNorm);

        public virtual string GetPredictedData(PlanInfoDto normPlanInfo, NormConfigDto userNorm)
        {
            var random = new Random(unchecked((int)DateTime.Now.Ticks));
            var predictedDatas = GetPredictedDataList(normPlanInfo, userNorm);

            if (predictedDatas.Count <= userNorm.ForecastCount)
            {
                return predictedDatas.Take(userNorm.ForecastCount).ToSplitString();
            }
            else
            {
                var skipMaxCount = (int)Math.Floor((double)(predictedDatas.Count - userNorm.ForecastCount) / 2);
                var skipCount = random.Next(0, skipMaxCount);
                return predictedDatas.Skip(skipCount).Take(userNorm.ForecastCount).ToSplitString();
            }

        }
    }
}