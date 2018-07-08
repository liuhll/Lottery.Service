using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Collections;

namespace Lottery.Engine.ComputePredictResult
{
    public class ZhiHeComputePredictResult : BasePredictData
    {
        private string[] zhiHeVal = new[] { "质", "合" };
        public ZhiHeComputePredictResult(IDictionary<int, double> predictedDataRate) : base(predictedDataRate)
        {
        }

        protected override ICollection<string> GetPredictedDataList(PlanInfoDto normPlanInfo, NormConfigDto userNorm)
        {
            // var perdictedVal = new List<string>();

            var perdictedVal = zhiHeVal.ToList().Shuffle().ToList();
            return perdictedVal;
        }
    }
}
