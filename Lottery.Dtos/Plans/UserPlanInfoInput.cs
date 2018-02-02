using System.Collections.Generic;

namespace Lottery.Dtos.Plans
{
    public class UserPlanInfoInput
    {
        /// <summary>
        /// 用户选中的计划Ids
        /// </summary>
        public ICollection<string> PlanIds { get; set; }
    }
}