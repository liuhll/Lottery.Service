using AutoMapper.Attributes;

namespace Lottery.Dtos.Lotteries
{
    [MapsFrom(typeof(PredictDataDto))]
    public class PredictDataDetail : PredictDataDto
    {
        public string LotteryData { get; set; }
    }
}