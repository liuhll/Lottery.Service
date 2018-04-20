namespace Lottery.Dtos.Norms
{
    public class UserNormDefaultConfigInput
    {
        /// <summary>
        /// 定码个数
        /// </summary>
        public int ForecastCount { get; set; }

        /// <summary>
        /// 周期数
        /// </summary>
        public int PlanCycle { get; set; }

        /// <summary>
        /// 偏差历史期数
        /// </summary>
        public int UnitHistoryCount { get; set; }

        /// <summary>
        /// 历史期数
        /// </summary>
        public int HistoryCount { get; set; }

        /// <summary>
        /// 最小连对数
        /// </summary>
        public int MinRightSeries { get; set; }

        /// <summary>
        /// 最大连对数
        /// </summary>
        public int MaxRightSeries { get; set; }

        /// <summary>
        /// 最小连错数
        /// </summary>
        public int MinErrorSeries { get; set; }

        /// <summary>
        /// 最大连错数
        /// </summary>
        public int MaxErrorSeries { get; set; }

        /// <summary>
        /// 追号的期数
        /// </summary>
        public int LookupPeriodCount { get; set; }

        /// <summary>
        /// 期望的最小成绩
        /// </summary>
        public int ExpectMinScore { get; set; }

        /// <summary>
        /// 期望的最大成绩
        /// </summary>
        public int ExpectMaxScore { get; set; }

        //public string CustomNumbers { get; set; }
    }
}