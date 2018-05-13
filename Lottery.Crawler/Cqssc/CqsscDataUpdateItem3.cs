using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using EasyHttp.Http;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Extensions;

namespace Lottery.Crawler.Cqssc
{
    public class CqsscDataUpdateItem3 : BaseDataUpdateItem
    {
        public CqsscDataUpdateItem3(DataSiteDto dataSite) : base(dataSite)
        {
        }

        protected override IList<LotteryDataDto> RequestDatas(int finalData)
        {
            _httpClient.Request.Accept = HttpContentTypes.ApplicationJson;
            _httpClient.Request.UserAgent = CHROME_UA;
            var response = _httpClient.Get(_dataSite.Url);
            var crawlData = response.DynamicBody;
            if (crawlData != null && crawlData.errorCode == 0)
            {
                var resultList = new List<LotteryDataDto>();
                var lotteryDatas = crawlData.result.data;
                foreach (var data in lotteryDatas)
                {
                    string periodStr = data.preDrawIssue.ToString();
                    periodStr = periodStr.Substring(2);
                    var period = Convert.ToInt32(periodStr);
                    if (period > finalData)
                    {
                        var lotteryData = new LotteryDataDto()
                        {
                            LotteryId = _dataSite.LotteryId,
                            Period = period,
                            LotteryTime = Convert.ToDateTime(data.preDrawTime),
                            Data = data.preDrawCode
                        };
                        resultList.Add(lotteryData);
                    }


                }

                return resultList;
            }
           
            return null;
        }

    }
}