using System;
using System.Collections.Generic;
using System.Diagnostics;
using EasyHttp.Http;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Extensions;

namespace Lottery.Crawler.Bjpks
{
    public class BjpksDataUpdateItem1 : BaseDataUpdateItem
    {

        public BjpksDataUpdateItem1(DataSiteDto dataSite) : base(dataSite)
        {
        }


        protected override IList<LotteryDataDto> RequestDatas(int finalData)
        {

            _httpClient.Request.Accept = HttpContentTypes.ApplicationJson;
            _httpClient.Request.UserAgent = CHROME_UA;
            var response = _httpClient.Get(_dataSite.Url, new { gameCode = "bjpk10" });
            var crawlData = response.DynamicBody;

            var period = Convert.ToInt32(crawlData.preIssue);
            if (period > finalData)
            {
                var resultList = new List<LotteryDataDto>();

                var openNumArr = crawlData.openNum;

                string dataStr = String.Empty;
                foreach (var number in openNumArr)
                {
                    dataStr += number + ",";
                }
                Debug.Assert(!string.IsNullOrEmpty(dataStr));
                dataStr = dataStr.Remove(dataStr.Length - 1);

                var lotteryData = new LotteryDataDto()
                {
                    LotteryId = _dataSite.LotteryId,
                    Period = period,
                    LotteryTime = DateTimeExtensions.TimeStampConvetDateTime(Convert.ToInt64(crawlData.currentOpenDateTime) / 1000),
                    Data = dataStr
                };

                resultList.Add(lotteryData);
                return resultList;
            }
            return null;

        }
    }
}