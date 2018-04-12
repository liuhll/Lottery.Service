using System.Collections.Generic;
using System.Linq;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Extensions;

namespace Lottery.Engine.ComputePredictResult
{
    public class RankComputePredictResult : BasePredictData
    {
        private string[] valList = new[]
        {
            "冠军",
            "亚军",
            "季军",
            "第四名",
            "第五名",
            "第六名",
            "第七名",
            "第八名",
            "第九名",
            "第十名",
        };
        public RankComputePredictResult(IDictionary<int, double> predictedDataRate) : base(predictedDataRate)
        {
        }


        protected override ICollection<string> GetPredictedDataList(PlanInfoDto normPlanInfo, NormConfigDto userNorm)
        {
            var resultVal = new List<string>();
            if (normPlanInfo.DsType == PredictType.Fix)
            {
                var percentRate  = _predictedDataRate.OrderByDescending(p => p.Value);
                foreach (var item in percentRate)
                {
                    resultVal.Add(valList[item.Key - 1]);
                }
            }
            else
            {
                var percentRate = _predictedDataRate.OrderBy(p => p.Value);
                foreach (var item in percentRate)
                {
                    resultVal.Add(valList[item.Key - 1]);
                }
            }
            return resultVal.ToList();
        }
    }
}