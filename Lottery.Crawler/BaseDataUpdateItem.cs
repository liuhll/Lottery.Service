using System;
using System.Collections.Generic;
using EasyHttp.Http;
using ECommon.Components;
using ECommon.Logging;
using Lottery.Dtos.Lotteries;

namespace Lottery.Crawler
{
    public abstract class BaseDataUpdateItem : IDataUpdateItem
    {
        protected DataSiteDto _dataSite;
        protected HttpClient _httpClient;
        protected ILogger _logger;

        protected const string CHROME_UA = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36";

        protected readonly object _lockObj = new object();


        protected BaseDataUpdateItem(DataSiteDto dataSite)
        {
            _dataSite = dataSite;
            _logger = _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(this.GetType()); ;
            _httpClient = new HttpClient();
        }

        public virtual IList<LotteryDataDto> CrawlDatas(int finalData)
        {
            try
            {
                lock (_lockObj)
                {
                    return RequestDatas(finalData);
                }
                
            }
            catch (Exception e)
            {
               _logger.Error(e.Message);
                return null;
            }
        }

        protected abstract IList<LotteryDataDto> RequestDatas(int finalData);
    }
}