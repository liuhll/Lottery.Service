using System.Collections.Generic;

namespace Lottery.Dtos.Plans
{
    public class UserPlanInfoInput
    {
        /// <summary>
        /// 彩种Id
        /// </summary>
        public string LotteryId { get; set; }

        /// <summary>
        /// 用户选中的计划Ids
        /// </summary>
        public ICollection<string> PlanIds { get; set; }
    }
}