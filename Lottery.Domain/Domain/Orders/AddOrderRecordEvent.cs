using ENode.Eventing;
using Lottery.Infrastructure.Enums;

namespace Lottery.Core.Domain.Orders
{
    public class AddOrderRecordEvent : DomainEvent<string>
    {
        private AddOrderRecordEvent()
        {
        }

        public AddOrderRecordEvent(string salesOrderNo,string goodsId, string authRankId, string lotteryId, OrderSourceType orderSourceType,
            int count, double unitPrice, double originalCost, double orderCost, SellType amountType, string createBy, int status)
        {
            SalesOrderNo = salesOrderNo;
            GoodsId = goodsId;
            AuthRankId = authRankId;
            LotteryId = lotteryId;
            OrderSourceType = orderSourceType;
            Count = count;
            UnitPrice = unitPrice;
            OriginalCost = originalCost;
            OrderCost = orderCost;
            AmountType = amountType;
            CreateBy = createBy;
            Status = status;
        }

        public string GoodsId { get; private set; }

        public string SalesOrderNo { get; private set; }

        public string AuthRankId { get; private set; }

        public string LotteryId { get; private set; }

        public OrderSourceType OrderSourceType { get; private set; }

        public int Count { get; private set; }

        public double UnitPrice { get; private set; }

        public double OriginalCost { get; private set; }

        public double OrderCost { get; private set; }

        public SellType AmountType { get; private set; }

        public string CreateBy { get; private set; }

        public int Status { get; private set; }
    }
}