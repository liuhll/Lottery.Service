using System.Collections.Generic;

namespace Lottery.Dtos.Sells
{
    public class OrderOutput
    {
        public ICollection<OrderInfoItem> OrderInfo { get; set; }

        public double OrderPrice { get; set; }

        public string OrderNo { get; set; }
    }
}