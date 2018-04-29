namespace Lottery.Dtos.Sells
{
    public class NotifyCallBackInput
    {
        public string Paysapi_id { get; set; }

        public string Orderid { get; set; }

        public double Price { get; set; }

        public double Realprice { get; set; }

        public string Orderuid { get; set; }

        public string Key { get; set; }
    }
}