using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Sells
{
    public class PayOutput
    {
        public string Msg { get; set; }

        public string QrCodeImageAddress { get; set; }

        public string QrCode { get; set; }

        public PayType IsType { get; set; }

        public string RealPrice { get; set; }
    }
}