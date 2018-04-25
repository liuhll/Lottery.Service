using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Extensions;

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

        public string PurchaseTypeDesc => PurchaseType.GetChineseDescribe();

        public double Discount { get; set; } = 1.00;
    }
}