using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Sells
{
    public class PointPayInput
    {
        public string OrderId { get; set; }

        public string GoodsName { get; set; }

        public int Price { get; set; }
    }
}