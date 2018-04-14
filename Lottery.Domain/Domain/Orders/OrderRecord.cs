using System;
using ENode.Domain;
using Lottery.Core.Domain.Points;
using Lottery.Infrastructure.Enums;

namespace Lottery.Core.Domain.Orders
{
    public class OrderRecord : AggregateRoot<string>
    {
        public OrderRecord(string id, string salesOrderNo, string authRankId, string lotteryId, OrderSourceType orderSourceType,
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
            CreateTime = DateTime.Now;
            Status = 0;
            ApplyEvent(new AddOrderRecordEvent(salesOrderNo,authRankId,lotteryId,orderSourceType,count,unitPrice,originalCost,orderCost,amountType,createBy, Status));
        }

        public string PayOrderNo { get; private set; }

        public string ThirdPayOrderNo { get; private set; }

        public DateTime? PayTime { get; private set; }

        public PayType PayType { get; private set; }

        public string Payer { get; private set; }

        public string SalesOrderNo { get; private set; }

        public string AuthRankId { get; private set; }

        public string LotteryId { get; private set; }

        public int OrderSourceType { get; private set; }

        public int Count { get; private set; }

        public double UnitPrice { get; private set; }

        public double OriginalCost { get; private set; }

        public double OrderCost { get; private set; }

        public SellType AmountType { get; private set; }

        public string CreateBy { get; private set; }
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string UpdateBy { get; private set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? UpdateTime { get; private set; }

        public int Status { get; private set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; private set; }

        #region Handle

        private void Handle(AddOrderRecordEvent evnt)
        {
            SalesOrderNo = evnt.SalesOrderNo;
            AuthRankId = evnt.AuthRankId;
            LotteryId = evnt.LotteryId;
            OrderSourceType = evnt.OrderSourceType;
            Count = evnt.Count;
            UnitPrice = evnt.UnitPrice;
            OriginalCost = evnt.OriginalCost;
            OrderCost = evnt.OrderCost;
            AmountType = evnt.AmountType;
            CreateBy = evnt.CreateBy;
            CreateTime = evnt.Timestamp;
        }

        #endregion

    }
}