namespace Lottery.Infrastructure.Enums
{
    /// <summary>
    /// 开奖状态
    /// </summary>
    public enum LotteryRunStatus
    {
        /// <summary>
        /// 开奖完成,获取到最新一次开奖结果
        /// </summary>
        LotteryStarted = 1,

        /// <summary>
        /// 开奖数据已经完成
        /// </summary>
        LotteryCompleted = 2,

        /// <summary>
        /// 开始算下一期开奖结果
        /// </summary>
        CalculateNumStarted = 3,

        /// <summary>
        /// 下一期开奖结果计算完成
        /// </summary>
        CalculateNumCompleted = 4,
    }
}