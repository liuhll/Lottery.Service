using ENode.Domain;

namespace Lottery.Core.Domain.UserNormDefaultConfig
{
    public class UserNormDefaultConfig : AggregateRoot<string>
    {
        public UserNormDefaultConfig(string id, string userId, string lotteryId, int planCycle, int forecastCount,
            int unitHistoryCount, int historyCount, int minRightSeries, int maxRightSeries, int minErrorSeries, int maxErrorSeries,
            int lookupPeriodCount, int expectMinScore, int expectMaxScore) : base(id)
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
            CustomNumbers = null;

            //ApplyEvent(new AddUserNormDefaultConfigEvent(UserId, LotteryId, PlanCycle, ForecastCount, UnitHistoryCount, MaxRightSeries, MinRightSeries, MaxErrorSeries,
            //    MinErrorSeries, LookupPeriodCount, ExpectMaxScore, ExpectMinScore, CustomNumbers));

            ApplyEvent(new AddUserNormDefaultConfigEvent(this));
        }

        public string UserId { get; private set; }

        /// <summary>
        /// 彩种Id
        /// </summary>
        public string LotteryId { get; private set; }

        /// <summary>
        /// 周期数
        /// </summary>
        public int PlanCycle { get; private set; }

        public int ForecastCount { get; private set; }

        /// <summary>
        /// 历史期数
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

        /// <summary>
        /// 用户选定的彩票种子数据
        /// </summary>
        public string CustomNumbers { get; private set; }

        #region public Methods

        public void UpdateUserNormDefaultConfig(int planCycle, int forecastCount, int unitHistoryCount, int historyCount, int minRightSeries, int maxRightSeries, int minErrortSeries, int maxErrortSeries, int lookupPeriodCount, int expectMinScore, int expectMaxScore)
        {
            ApplyEvent(new UpdateUserNormDefaultConfigEvent(UserId, LotteryId, planCycle, forecastCount, unitHistoryCount, historyCount, minRightSeries, maxRightSeries, minErrortSeries, maxErrortSeries, lookupPeriodCount, expectMinScore, expectMaxScore));
        }

        #endregion public Methods

        #region Handler Methods

        private void Handle(AddUserNormDefaultConfigEvent evnt)
        {
            UserId = evnt.UserId;
            LotteryId = evnt.LotteryId;
            PlanCycle = evnt.PlanCycle;
            ForecastCount = evnt.ForecastCount;
            UnitHistoryCount = evnt.UnitHistoryCount;
            MaxRightSeries = evnt.MaxRightSeries;
            MinRightSeries = evnt.MinRightSeries;
            MaxErrorSeries = evnt.MaxErrorSeries;
            MinErrorSeries = evnt.MinErrorSeries;
            LookupPeriodCount = evnt.LookupPeriodCount;
            ExpectMaxScore = evnt.ExpectMaxScore;
            ExpectMinScore = evnt.ExpectMinScore;
            CustomNumbers = evnt.CustomNumbers;
            HistoryCount = evnt.HistoryCount;
        }

        private void Handle(UpdateUserNormDefaultConfigEvent evnt)
        {
            PlanCycle = evnt.PlanCycle;
            ForecastCount = evnt.ForecastCount;
            UnitHistoryCount = evnt.UnitHistoryCount;
            MaxRightSeries = evnt.MaxRightSeries;
            MinRightSeries = evnt.MinRightSeries;
            MaxErrorSeries = evnt.MaxErrorSeries;
            MinErrorSeries = evnt.MinErrorSeries;
            LookupPeriodCount = evnt.LookupPeriodCount;
            ExpectMaxScore = evnt.ExpectMaxScore;
            ExpectMinScore = evnt.ExpectMinScore;
            HistoryCount = evnt.HistoryCount;
        }

        #endregion Handler Methods
    }
}