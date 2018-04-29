using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Sells
{
    public class OrderInfoDto
    {
        public string SalesOrderNo { get; set; }

        public string GoodsId { get; set; }
        public string AuthRankId { get; set; }

        public string LotteryId { get; set; }

        public OrderSourceType OrderSourceType { get; set; }

        public int Count { get; set; }

        public double UnitPrice { get; set; }

        public double OriginalCost { get; set; }

        public double OrderCost { get; set; }

        public SellType AmountType { get; set; }
    }
}