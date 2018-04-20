using ENode.Commanding;

namespace Lottery.Commands.Norms
{
    public class AddNormConfigCommand : Command<string>
    {
        private AddNormConfigCommand()
        {
        }

        public AddNormConfigCommand(string id, string userId, string lotteryId, string planId, int planCycle, int forecastCount, int lastStartPeriod,
            int unitHistoryCount, int historyCount, int minRightSeries, int maxRightSeries, int minErrorSeries, int maxErrorSeries,
            int lookupPeriodCount, int expectMinScore, int expectMaxScore, int sort, string customNumbers = null) : base(id)
        {
            UserId = userId;
            LotteryId = lotteryId;
            PlanId = planId;
            LastStartPeriod = lastStartPeriod;
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
            Sort = sort;
            CustomNumbers = customNumbers;
        }

        public string UserId { get; private set; }

        public string LotteryId { get; private set; }

        public string PlanId { get; private set; }

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

        /// <summary>
        /// 历史周期
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

        public int Sort { get; private set; }

        public string CustomNumbers { get; set; }
    }
}