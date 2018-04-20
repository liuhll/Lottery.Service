using Lottery.Dtos.Lotteries;
using System.Collections.Generic;

namespace Lottery.Crawler
{
    public interface IDataUpdateItem
    {
        IList<LotteryDataDto> CrawlDatas(int finalData);
    }
}