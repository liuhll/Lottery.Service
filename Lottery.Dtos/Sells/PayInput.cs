using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Sells
{
    public class PayInput
    {
        public string OrderId { get; set; }

        public PayType IsType { get; set; }

        public string GoodsName { get; set; }

        public double Price { get; set; }
    }
}