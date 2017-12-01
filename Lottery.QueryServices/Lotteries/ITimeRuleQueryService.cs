using System.Collections.Generic;
using Lottery.Dtos.Lotteries;

namespace Lottery.QueryServices.Lotteries
{
    public interface ITimeRuleQueryService
    {
        ICollection<TimeRuleDto> GetTimeRules(string lotteryId);
    }
}