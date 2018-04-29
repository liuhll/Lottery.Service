using ENode.Domain;

namespace Lottery.Core.Domain.DataSites
{
    public class DataSite : AggregateRoot<string>
    {
        public DataSite(string id, string lotteryId, string name, string url, int count, string crawlType, bool status, int sort)
            : base(id)
        {
            this.LotteryId = lotteryId;
            this.Name = name;
            this.Url = url;
            this.Count = count;
            this.CrawlType = crawlType;
            this.Status = status;
            this.Sort = sort;
        }

        public string LotteryId { get; private set; }

        public string Name { get; private set; }

        public string Url { get; private set; }

        public int Count { get; private set; }

        public string CrawlType { get; private set; }

        public bool Status { get; private set; }

        public int Sort { get; private set; }
    }
}