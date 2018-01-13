using System.Collections.Generic;

namespace Lottery.Dtos.Plans
{
    public class NormGroupOutput
    {
        public string Id { get; set; }

        public string GroupCode { get; set; }

        public string GroupName { get; set; }

        public ICollection<PlanInfoOutput> PlanInfos { get; set; }
    }
}