using System;
using System.Globalization;
using ENode.Domain;

namespace Lottery.Core.Domain.NormConfigs
{
    public class NormConfig : AggregateRoot<string>
    {
        public NormConfig(
          string id,
          string userId,
          string lotteryId,
          string planId,
          int lastStartPeriod,
          int planCycle,
          int forecastCount,
          int unitHistoryCount,
          int minRightSeries, int maxRightSeries, int minErrorSeries, int maxErrorSeries,
          int lookupPeriodCount, int expectMinScore, int expectMaxScore,
          int sort
          ) : base(id)
        {
            UserId = userId;
            PlanId = planId;
            LotteryId = lotteryId;
            PlanCycle = planCycle;
            ForecastCount = forecastCount;
            UnitHistoryCount = unitHistoryCount;
            LastStartPeriod = lastStartPeriod;
            UnitHistoryCount = unitHistoryCount;
            MaxRightSeries = maxRightSeries;
            MinRightSeries = minRightSeries;
            MaxErrorSeries = maxErrorSeries;
            MinErrorSeries = minErrorSeries;
            LookupPeriodCount = lookupPeriodCount;
            ExpectMaxScore = expectMaxScore;
            ExpectMinScore = expectMinScore;
            IsEnable = true;
            IsDefualt = false;
            Sort = sort;

            ApplyEvent(new AddNormConfigEvent(this));
        }

        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; private set; }

        public string LotteryId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string PlanId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int PlanCycle { get; private set; }

        public int LastStartPeriod { get; private set; }

        /// <summary>
        /// 
        /// </summary>
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
        /// 
        /// </summary>
        public bool IsEnable { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDefualt { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreateBy { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string UpdateBy { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateTime { get; private set; }

        public int Sort { get; private set; }

        #region public Methods
        public void DeleteNormConfig(string aggregateRootId)
        {
           ApplyEvent(new DeleteNormConfigEvent(UserId,LotteryId));
        }


        public void UpdateNormConfig(int lastStartPeriod, int planCycle, int forecastCount, int unitHistoryCount, int minRightSeries,
            int maxRightSeries, int minErrortSeries, int maxErrortSeries, int lookupPeriodCount, int expectMinScore, int expectMaxScore, string customNumbers)
        {
            ApplyEvent(new UpdateNormConfigEvent(UserId, LotteryId, lastStartPeriod,
                planCycle, forecastCount, unitHistoryCount, minRightSeries, maxRightSeries, minErrortSeries,
                maxErrortSeries, lookupPeriodCount, expectMinScore, expectMaxScore, customNumbers));
        }

        #endregion

        #region handle Methods

        private void Handle(AddNormConfigEvent evnt)
        {

            UserId = evnt.UserId;
            LotteryId = evnt.LotteryId;
            PlanId = evnt.PlanId;
            LastStartPeriod = evnt.LastStartPeriod;
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
            IsEnable = evnt.IsEnable;
            IsDefualt = evnt.IsDefualt;
        }

        private void Handle(UpdateNormConfigEvent evnt)
        {

            UserId = evnt.UserId;
            LotteryId = evnt.LotteryId;
            LastStartPeriod = evnt.LastStartPeriod;
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
        }

        private void Handle(DeleteNormConfigEvent evnt)
        {
            UserId = evnt.UserId;
            LotteryId = evnt.LotteryId;
        }

        #endregion

    }
}
