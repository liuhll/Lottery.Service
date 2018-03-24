using ENode.Eventing;

namespace Lottery.Core.Domain.NormConfigs
{
    public class AddNormConfigEvent : DomainEvent<string>
    {
        private AddNormConfigEvent()
        {
        }

        public AddNormConfigEvent(NormConfig normConfig)
        {
            UserId = normConfig.UserId;
            LotteryId = normConfig.LotteryId;
            PlanId = normConfig.PlanId;
            LastStartPeriod = normConfig.LastStartPeriod;
            PlanCycle = normConfig.PlanCycle;
            ForecastCount = normConfig.ForecastCount;
            UnitHistoryCount = normConfig.UnitHistoryCount;
            MaxRightSeries = normConfig.MaxRightSeries;
            MinRightSeries = normConfig.MinRightSeries;
            MaxErrorSeries = normConfig.MaxErrorSeries;
            MinErrorSeries = normConfig.MinErrorSeries;
            LookupPeriodCount = normConfig.LookupPeriodCount;
            ExpectMaxScore = normConfig.ExpectMaxScore;
            ExpectMinScore = normConfig.ExpectMinScore;
            IsEnable = normConfig.IsEnable;
            IsDefualt = normConfig.IsDefualt;
            Sort = normConfig.Sort;
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

        public int Sort { get; private set; }
    }
}