﻿using ENode.Eventing;

namespace Lottery.Core.Domain.UserNormDefaultConfig
{
    public class AddUserNormDefaultConfigEvent : DomainEvent<string>
    {
        private AddUserNormDefaultConfigEvent()
        {
        }

        public AddUserNormDefaultConfigEvent(string userId, string lotteryId, int planCycle, int forecastCount,
            int unitHistoryCount, int minRightSeries, int maxRightSeries, int minErrortSeries, int maxErrortSeries,
            int lookupPeriodCount, int expectMinScore, int expectMaxScore, string customNumbers)
        {
            UserId = userId;
            LotteryId = lotteryId;
            PlanCycle = planCycle;
            ForecastCount = forecastCount;
            UnitHistoryCount = unitHistoryCount;
            MaxRightSeries = maxRightSeries;
            MinRightSeries = minRightSeries;
            MaxErrortSeries = maxErrortSeries;
            MinErrortSeries = minErrortSeries;
            LookupPeriodCount = lookupPeriodCount;
            ExpectMaxScore = expectMaxScore;
            ExpectMinScore = expectMinScore;
            CustomNumbers = customNumbers;

        }

        public AddUserNormDefaultConfigEvent(UserNormDefaultConfig userNorm)
        {
            UserId = userNorm.UserId;
            LotteryId = userNorm.LotteryId;
            PlanCycle = userNorm.PlanCycle;
            ForecastCount = userNorm.ForecastCount;
            UnitHistoryCount = userNorm.UnitHistoryCount;
            MaxRightSeries = userNorm.MaxRightSeries;
            MinRightSeries = userNorm.MinRightSeries;
            MaxErrortSeries = userNorm.MaxErrortSeries;
            MinErrortSeries = userNorm.MinErrortSeries;
            LookupPeriodCount = userNorm.LookupPeriodCount;
            ExpectMaxScore = userNorm.ExpectMaxScore;
            ExpectMinScore = userNorm.ExpectMinScore;
            CustomNumbers = userNorm.CustomNumbers;


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
        public int MinErrortSeries { get; private set; }

        /// <summary>
        /// 最大连错数
        /// </summary>
        public int MaxErrortSeries { get; private set; }

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

        public string CustomNumbers { get; private set; }
    }
}