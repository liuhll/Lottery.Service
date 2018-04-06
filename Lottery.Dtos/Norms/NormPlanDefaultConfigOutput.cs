using System.Collections.Generic;

namespace Lottery.Dtos.Norms
{
    public class NormPlanDefaultConfigOutput
    {
        public ICollection<int> ForecastCounts { get; set; }

        public ICollection<int> PlanCycles { get; set; }
    }
}