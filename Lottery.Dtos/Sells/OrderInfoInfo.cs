using System;
using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Sells
{
    public class OrderInfoInfo : OrderInfoDto
    {
        public string Id { get; set; }

        public string PayOrderNo { get; set; }

        public string ThirdPayOrderNo { get; set; }

        public double PayCost { get; set; }

        public DateTime PayTime { get; set; }

        public PayType PayType { get; set; }

        public string Payer{ get; set; }

        public PayStatus Status { get; set; }
    }
}