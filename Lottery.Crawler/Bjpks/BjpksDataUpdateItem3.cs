using System;
using System.Collections.Generic;
using EasyHttp.Http;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Extensions;

namespace Lottery.Crawler.Bjpks
{
    public class BjpksDataUpdateItem3 : BaseDataUpdateItem
    {
        public BjpksDataUpdateItem3(DataSiteDto dataSite) : base(dataSite)
        {
        }

        protected override IList<LotteryDataDto> RequestDatas(int finalData)
        {
            _httpClient.Request.Accept = HttpContentTypes.ApplicationJson;
            var response = _httpClient.Get(_dataSite.Url);
            var crawlResult = response.RawText.ToObject();

            var period = Convert.ToInt32(crawlResult.current.periodNumber);
            if (period > finalData)
            {
                var resultList = new List<LotteryDataDto>();
                var lotteryData = new LotteryDataDto()
                {
                    LotteryId = _dataSite.LotteryId,
                    Period = period,
                    LotteryTime = Convert.ToDateTime(crawlResult.current.awardTime),
                    Data = crawlResult.current.awardNumbers
                };
                resultList.Add(lotteryData);

                return resultList;
            }

            return null;
        }
    }
}