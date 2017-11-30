using System;
using System.Collections.Generic;
using Lottery.QueryServices.Lotteries;

namespace Lottery.Engine.TimeRule
{
    public interface ITimeRuleManager
    {
        //:todo 需要重构,因为某个彩种的开奖规则可能不止一个

        /// <summary>
        /// 该彩种的开奖规则
        /// </summary>
        ICollection<TimeRuleDto> TimeRules { get; }

        DateTime? NextLotteryTime();

        /// <summary>
        /// 下一期开奖时间
        /// </summary>
        /// <param name="nextLotteryTime"></param>
        /// <returns></returns>
        bool NextLotteryTime(out DateTime nextLotteryTime);

        /// <summary>
        /// 今日总共将开出
        /// </summary>
        int TodayTotalCount { get; }

        /// <summary>
        /// 今日开奖的总期数
        /// </summary>
        int TodayCurrentCount { get; }

        /// <summary>
        /// 当前时间是否处于开奖期间
        /// </summary>
        bool IsLotteryDuration { get; }

        /// <summary>
        /// 今天的开奖规则
        /// </summary>
        TimeRuleDto TodayTimeRule { get; }

        /// <summary>
        /// 是否是最后一期
        /// </summary>
        bool IsFinalPeriod { get; }


    }
}