using ENode.Commanding;
using Lottery.Infrastructure.Enums;

namespace Lottery.Commands.Sells
{
    public class AddOrderRecordCommand : Command<string>
    {
        private AddOrderRecordCommand()
        {
        }

        public AddOrderRecordCommand(string id, string salesOrderNo, string authRankId, string lotteryId, OrderSourceType orderSourceType,
            int count, double unitPrice, double originalCost, double orderCost, SellType amountType, string createBy) : base(id)
        {
            SalesOrderNo = salesOrderNo;
            AuthRankId = authRankId;
            LotteryId = lotteryId;
            OrderSourceType = orderSourceType;
            Count = count;
            UnitPrice = unitPrice;
            OriginalCost = originalCost;
            OrderCost = orderCost;
            AmountType = amountType;
            CreateBy = createBy;
        }

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
    }
}