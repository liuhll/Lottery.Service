using System.Collections.Generic;

namespace Lottery.Dtos.Plans
{
    public class UserPlanInfoDto
    {
        public ICollection<PlanInfoOutput> UserSelectedPlanInfos { get; set; }

        public ICollection<NormGroupOutput> AllPlanInfos { get; set; }
    }
}