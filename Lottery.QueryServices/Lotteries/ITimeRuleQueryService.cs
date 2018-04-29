using Lottery.Dtos.Lotteries;
using System.Collections.Generic;

namespace Lottery.QueryServices.Lotteries
{
    public interface ITimeRuleQueryService
    {
        ICollection<TimeRuleDto> GetTimeRules(string lotteryId);
    }
}