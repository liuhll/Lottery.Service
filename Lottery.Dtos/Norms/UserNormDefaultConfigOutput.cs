using System.Collections.Generic;
using AutoMapper.Attributes;

namespace Lottery.Dtos.Norms
{
    [MapsTo(typeof(UserPlanNormOutput))]
    public class UserNormDefaultConfigOutput
    {
        /// <summary>
        /// 周期数
        /// </summary>
        public int PlanCycle { get; set; }

        public int ForecastCount { get; set; }

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

        /// <summary>
        /// 用户选定的彩票种子数据
        /// </summary>
        public string CustomNumbers { get; set; }

        public IList<LotteryNumber> LotteryNumbers { get; set; }

        /// <summary>
        /// 是否系统默认
        /// </summary>
        public bool IsDefualt { get; set; }

    }
}