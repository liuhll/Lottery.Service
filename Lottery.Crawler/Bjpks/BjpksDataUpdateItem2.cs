using System.Collections.Generic;
using Lottery.QueryServices.Lotteries;

namespace Lottery.Crawler.Bjpks
{
    public class BjpksDataUpdateItem2 : BaseDataUpdateItem
    {
        public BjpksDataUpdateItem2(DataSiteDto dataSite) : base(dataSite)
        {
        }

        public override IList<LotteryDataDto> CrawlDatas(int finalData)
        {
            return null;
        }
    }
}