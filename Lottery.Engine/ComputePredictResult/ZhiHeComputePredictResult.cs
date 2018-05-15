using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.Dtos.Lotteries;

namespace Lottery.Engine.ComputePredictResult
{
    public class ZhiHeComputePredictResult : BasePredictData
    {
        public ZhiHeComputePredictResult(IDictionary<int, double> predictedDataRate) : base(predictedDataRate)
        {
        }

        protected override ICollection<string> GetPredictedDataList(PlanInfoDto normPlanInfo, NormConfigDto userNorm)
        {
            throw new NotImplementedException();
        }
    }
}
