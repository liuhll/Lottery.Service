using AutoMapper.Attributes;

namespace Lottery.Dtos.Lotteries
{
    [MapsFrom(typeof(LotteryInfoDto))]
    public class LotteryInfoOutput
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
    }
}