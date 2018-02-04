using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Sells
{
    public class GoodInfoDto
    {
        public string GoodsName { get; set; }

        public double Price { get; set; }

        public double OriginalPrice { get; set; }

        public int Count { get; set; }

        public PurchaseType PurchaseType { get; set; }
    }
}