using System.Collections.Generic;
using System.Linq;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Extensions;

namespace Lottery.Engine.ComputePredictResult
{
    public class LhComputePredictResult : BasePredictData
    {
        private const string longVal = "龙";
        private const string huVal = "虎";
        public LhComputePredictResult(IDictionary<int, double> predictedDataRate) : base(predictedDataRate)
        {
        }


        public override string GetPredictedData(PlanInfoDto normPlanInfo, NormConfigDto userNorm)
        {
            var positionInfo = normPlanInfo.PositionInfos.First();

            var valArr = PredictVals(positionInfo.Position, normPlanInfo.DsType);
            return valArr.Take(userNorm.ForecastCount).ToSplitString(",");
        }

        private List<string> PredictVals(int position,PredictType predictType,int maxPosition = 10)
        {
            var valArr = new List<string>();
            if (_predictedDataRate[position] > _predictedDataRate[maxPosition])
            {
                if (predictType == PredictType.Fix)
                {
                    valArr.Add(longVal);
                    valArr.Add(huVal);
                }
                else
                {
                    valArr.Add(huVal);
                    valArr.Add(longVal);
                }
            }
            return valArr;
        }
    }
}