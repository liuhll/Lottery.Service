using System;

namespace Lottery.Dtos.Lotteries
{
    public class LotteryInfoDto
    {
        /// <summary>
        /// LotteryId
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 彩种编码
        /// </summary>
        public string LotteryCode { get; set; }

        /// <summary>
        /// 彩种名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分表策略;1.按月;2.按季度;3.按年
        /// </summary>
        public int? TableStrategy { get; set; }

        public int Status { get; set; }

        /// <summary>
        /// 是否完成动态分表的配置
        /// </summary>
        public bool IsCompleteDynamicTable { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}