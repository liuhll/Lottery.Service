using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Sells
{
    public class OrderInput
    {
        public string GoodId { get; set; }

        public double UnitPrice { get; set; }

        public double Discount { get; set; }

        public int Count { get; set; }

        public SellType SellType { get; set; }
    }
}