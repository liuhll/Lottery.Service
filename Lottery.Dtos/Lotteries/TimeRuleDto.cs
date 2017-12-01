namespace Lottery.Dtos.Lotteries
{
    public class TimeRuleDto
    {
        /// <summary>
        /// 彩种Id
        /// </summary>
        public string LotteryId { get; set; }

        //public string LotteryCode { get; set; }

        /// <summary>
        /// 星期
        /// </summary>
        public int Weekday { get; set; }

        /// <summary>
        /// 开奖开始时间
        /// </summary>
        public System.TimeSpan StartTime { get; set; }

        /// <summary>
        /// 开奖结束时间
        /// </summary>
        public System.TimeSpan EndTime { get; set; }

        /// <summary>
        /// 时间间隔
        /// </summary>
        public System.TimeSpan Tick { get; set; }
    }
}