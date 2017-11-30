namespace Lottery.QueryServices.Lotteries
{
    public class DataSiteDto
    {
        public string LotteryId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int Count { get; set; }

        public string CrawlType { get; set; }

        public bool Status { get; set; }

        public int Sort { get; set; }
    }
}