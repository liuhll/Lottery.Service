using System.Collections.Generic;
using Lottery.Dtos.Lotteries;

namespace Lottery.Crawler
{
    public interface IDataUpdateItem
    {
        IList<LotteryDataDto> CrawlDatas(int finalData);
    }
}