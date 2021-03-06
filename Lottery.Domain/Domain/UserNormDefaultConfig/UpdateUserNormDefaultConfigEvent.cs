﻿using ENode.Eventing;

namespace Lottery.Core.Domain.UserNormDefaultConfig
{
    public class UpdateUserNormDefaultConfigEvent : DomainEvent<string>
    {
        private UpdateUserNormDefaultConfigEvent()
        {
        }

        public UpdateUserNormDefaultConfigEvent(string userId, string lotteryId, int planCycle, int forecastCount,
            int unitHistoryCount, int historyCount, int minRightSeries, int maxRightSeries, int minErrorSeries, int maxErrorSeries,
            int lookupPeriodCount, int expectMinScore, int expectMaxScore)
        {
            UserId = userId;
            LotteryId = lotteryId;
            PlanCycle = planCycle;
            ForecastCount = forecastCount;
            UnitHistoryCount = unitHistoryCount;
            HistoryCount = historyCount;
            MaxRightSeries = maxRightSeries;
            MinRightSeries = minRightSeries;
            MaxErrorSeries = maxErrorSeries;
            MinErrorSeries = minErrorSeries;
            LookupPeriodCount = lookupPeriodCount;
            ExpectMaxScore = expectMaxScore;
            ExpectMinScore = expectMinScore;
        }

        public int ForecastCount { get; private set; }

        public string UserId { get; private set; }

        /// <summary>
        /// 彩种Id
        /// </summary>
        public string LotteryId { get; private set; }

        /// <summary>
        /// 周期数
        /// </summary>
        public int PlanCycle { get; private set; }

        /// <summary>
        /// 历史期数
        /// </summary>
        public int UnitHistoryCount { get; private set; }

        /// <summary>
        /// 历史期数
        /// </summary>
        public int HistoryCount { get; private set; }

        /// <summary>
        /// 最小连对数
        /// </summary>
        public int MinRightSeries { get; private set; }

        /// <summary>
        /// 最大连对数
        /// </summary>
        public int MaxRightSeries { get; private set; }

        /// <summary>
        /// 最小连错数
        /// </summary>
        public int MinErrorSeries { get; private set; }

        /// <summary>
        /// 最大连错数
        /// </summary>
        public int MaxErrorSeries { get; private set; }

        /// <summary>
        /// 追号的期数
        /// </summary>
        public int LookupPeriodCount { get; private set; }

        /// <summary>
        /// 期望的最小成绩
        /// </summary>
        public int ExpectMinScore { get; private set; }

        /// <summary>
        /// 期望的最大成绩
        /// </summary>
        public int ExpectMaxScore { get; private set; }
    }
}