using ENode.Domain;

namespace Lottery.Core.Domain.UserNormDefaultConfig
{
    public class UserNormDefaultConfig : AggregateRoot<string>
    {
        public UserNormDefaultConfig(string id, string userId, string lotteryId, int planCycle,int forecastCount,
            int unitHistoryCount, int minRightSeries, int maxRightSeries, int minErrortSeries, int maxErrortSeries,
            int lookupPeriodCount, int expectMinScore, int expectMaxScore) : base(id)
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
            CustomNumbers = null;

            //ApplyEvent(new AddUserNormDefaultConfigEvent(UserId, LotteryId, PlanCycle, ForecastCount, UnitHistoryCount, MaxRightSeries, MinRightSeries, MaxErrortSeries,
            //    MinErrortSeries, LookupPeriodCount, ExpectMaxScore, ExpectMinScore, CustomNumbers));

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

        /// <summary>
        /// 用户选定的彩票种子数据
        /// </summary>
        public string CustomNumbers { get; private set; }


        #region public Methods

        public void UpdateUserNormDefaultConfig(int planCycle, int forecastCount, int unitHistoryCount, int minRightSeries, int maxRightSeries, int minErrortSeries, int maxErrortSeries, int lookupPeriodCount, int expectMinScore, int expectMaxScore)
        {
            ApplyEvent(new UpdateUserNormDefaultConfigEvent(UserId,LotteryId,planCycle,forecastCount,unitHistoryCount, minRightSeries, maxRightSeries, minErrortSeries, maxErrortSeries, lookupPeriodCount,expectMinScore,expectMaxScore));
        }

        #endregion

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
            MaxErrortSeries = evnt.MaxErrortSeries;
            MinErrortSeries = evnt.MinErrortSeries;
            LookupPeriodCount = evnt.LookupPeriodCount;
            ExpectMaxScore = evnt.ExpectMaxScore;
            ExpectMinScore = evnt.ExpectMinScore;
            CustomNumbers = evnt.CustomNumbers;
        }
        private void Handle(UpdateUserNormDefaultConfigEvent evnt)
        {
            PlanCycle = evnt.PlanCycle;
            ForecastCount = evnt.ForecastCount;
            UnitHistoryCount = evnt.UnitHistoryCount;
            MaxRightSeries = evnt.MaxRightSeries;
            MinRightSeries = evnt.MinRightSeries;
            MaxErrortSeries = evnt.MaxErrortSeries;
            MinErrortSeries = evnt.MinErrortSeries;
            LookupPeriodCount = evnt.LookupPeriodCount;
            ExpectMaxScore = evnt.ExpectMaxScore;
            ExpectMinScore = evnt.ExpectMinScore;
        }

        #endregion


    }
}