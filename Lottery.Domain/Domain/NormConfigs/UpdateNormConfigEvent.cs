using ENode.Eventing;

namespace Lottery.Core.Domain.NormConfigs
{
    public class UpdateNormConfigEvent : DomainEvent<string>
    {
        private UpdateNormConfigEvent()
        {
        }

        public UpdateNormConfigEvent(string userId, string lotteryId, int lastStartPeriod, int planCycle, int forecastCount, int unitHistoryCount,
            int historyCount, int minRightSeries,
            int maxRightSeries, int minErrortSeries, int maxErrortSeries, int lookupPeriodCount, int expectMinScore, int expectMaxScore, string customNumbers)
        {
            UserId = userId;
            LotteryId = lotteryId;
            LastStartPeriod = lastStartPeriod;
            PlanCycle = planCycle;
            ForecastCount = forecastCount;
            UnitHistoryCount = unitHistoryCount;
            HistoryCount = historyCount;
            MaxRightSeries = maxRightSeries;
            MinRightSeries = minRightSeries;
            MaxErrorSeries = maxErrortSeries;
            MinErrorSeries = minErrortSeries;
            LookupPeriodCount = lookupPeriodCount;
            ExpectMaxScore = expectMaxScore;
            ExpectMinScore = expectMinScore;
            CustomNumbers = customNumbers;
        }

        public string UserId { get; private set; }

        public string LotteryId { get; private set; }

        public int LastStartPeriod { get; private set; }

        public int ForecastCount { get; private set; }

        /// <summary>
        /// 周期数
        /// </summary>
        public int PlanCycle { get; private set; }

        /// <summary>
        /// 偏差历史期数
        /// </summary>
        public int UnitHistoryCount { get; private set; }

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

        public string CustomNumbers { get; private set; }
    }
}