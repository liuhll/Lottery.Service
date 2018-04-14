using System;
using System.Diagnostics;
using Lottery.Infrastructure.Enums;

namespace Lottery.Infrastructure.Tools
{
    public class OrderHelper
    {
        public static string GenerateOrderNo(OrderType orderType, SellType sellType)
        {
            var orderNo = string.Empty;
            var prefix = "";
            if (orderType == OrderType.Order)
            {
                switch (sellType)
                {
                    case SellType.Point:
                        prefix = "OP";
                        break;
                    case SellType.Rmb:
                        prefix = "OR";
                        break;
                }
            }
            else if (orderType == OrderType.Pay)
            {
                switch (sellType)
                {
                    case SellType.Point:
                        prefix = "PP";
                        break;
                    case SellType.Rmb:
                        prefix = "PR";
                        break;
                }
            }
            var date = DateTime.Now.ToString("yyyyMMdd");
            var timestamp = (int)(DateTime.Now - DateTime.Today).TotalSeconds;
            var saltFigure = RandomHelper.GenerateIdentifyCode(4);
            orderNo = prefix + date + timestamp + saltFigure;
            return orderNo;
        }
    }
}