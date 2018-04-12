using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Sells
{
    public class GoodsOutput
    {
        public string GoodsName { get; set; }

        public double Price => OriginalPrice * Discount;

        public double OriginalPrice { get; set; }

        public int Count { get; set; }

        public PurchaseType PurchaseType { get; set; }

        public double Discount { get; set; } = 1.00;
    }
}