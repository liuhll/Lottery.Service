using System;

namespace Lottery.Dtos.Lotteries
{
    /// <summary>
    /// 最后一期开奖信息
    /// </summary>
    public class FinalLotteryDataOutput
    {
        /// <summary>
        /// 最后一期是否开奖
        /// </summary>
        public bool IsLotteryData { get; set; }

        /// <summary>
        /// 最后一期开奖期数
        /// </summary>
        public int Period { get; set; }


        /// <summary>
        /// 开奖数据
        /// </summary>
        public string Data { get; set; }


        /// <summary>
        /// 最后一期开奖时间
        /// </summary>
        public DateTime LotteryTime { get; set; }

        /// <summary>
        /// 下一期开奖时间
        /// </summary>
        public DateTime? NextLotteryTime { get; set; }

        /// <summary>
        /// 下一期期数
        /// </summary>
        public int NextPeriod { get; set; }

        /// <summary>
        /// 下一期开奖剩余时间
        /// </summary>
        public int RemainSeconds { get; set; }
    }
}