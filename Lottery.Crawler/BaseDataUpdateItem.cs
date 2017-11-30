using System.Collections.Generic;
using EasyHttp.Http;
using Lottery.QueryServices.Lotteries;

namespace Lottery.Crawler
{
    public abstract class BaseDataUpdateItem : IDataUpdateItem
    {
        protected DataSiteDto _dataSite;
        protected HttpClient _httpClient;

        protected readonly object _lockObj = new object();


        protected BaseDataUpdateItem(DataSiteDto dataSite)
        {
            _dataSite = dataSite;
            _httpClient = new HttpClient();
        }

        public abstract IList<LotteryDataDto> CrawlDatas(int finalData);
    }
}