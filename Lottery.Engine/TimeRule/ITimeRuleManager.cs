using System;
using System.Collections.Generic;
using Lottery.QueryServices.Lotteries;

namespace Lottery.Engine.TimeRule
{
    public interface ITimeRuleManager
    {
        ICollection<TimeRuleDto> TimeRules { get; }

        DateTime? NextLotteryTime();

        bool NextLotteryTime(out DateTime nextLotteryTime);

        int TodayTotalCount { get; }

        //int TodayCurrentCount { get; }

        bool IsLotteryDuration { get; }

        TimeRuleDto TodayTimeRule { get; }


    }
}