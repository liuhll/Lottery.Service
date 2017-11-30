using System.Collections.Generic;
using Lottery.QueryServices.Lotteries;

namespace Lottery.Crawler
{
    public interface IDataUpdateItem
    {
        IList<LotteryDataDto> CrawlDatas(int finalData);
    }
}