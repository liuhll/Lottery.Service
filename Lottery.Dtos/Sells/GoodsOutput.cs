using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Sells
{
    public class GoodsOutput
    {
        public string GoodsId { get; set; }

        public string GoodsName { get; set; }

        public double SellPrice => OriginalPrice * Discount;

        public double OriginalPrice => UnitPrice * Count;

        public double UnitPrice { get; set; }

        public int Count { get; set; }

        public PurchaseType PurchaseType { get; set; }

        public double Discount { get; set; } = 1.00;
    }
}