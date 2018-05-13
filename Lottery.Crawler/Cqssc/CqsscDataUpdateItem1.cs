using System;
using System.Collections.Generic;
using System.Text;
using EasyHttp.Http;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Extensions;

namespace Lottery.Crawler.Cqssc
{
    public class CqsscDataUpdateItem1 : BaseDataUpdateItem
    {
        public CqsscDataUpdateItem1(DataSiteDto dataSite) : base(dataSite)
        {
        }

        protected override IList<LotteryDataDto> RequestDatas(int finalData)
        {
            _httpClient.Request.Accept = HttpContentTypes.ApplicationJson;
            _httpClient.Request.UserAgent = CHROME_UA;
            var response = _httpClient.Get(_dataSite.Url);
            var crawlData = response.DynamicBody;
            if (crawlData != null && crawlData.status == 1)
            {
                var lotteryDatas = crawlData.data.list;

                var resultList = new List<LotteryDataDto>();
                foreach (var item in lotteryDatas)
                {
                    string periodStr = item.issue.ToString();
                    periodStr = periodStr.Substring(2);
                    var period = Convert.ToInt32(periodStr);
                    if (period > finalData)
                    {
                        var lotteryData = new LotteryDataDto()
                        {
                            LotteryId = _dataSite.LotteryId,
                            Period = period,
                            LotteryTime = DateTimeExtensions.TimeStampConvetDateTime(Convert.ToInt64(item.kjtime)),
                            Data = GetLotteryData(item.kjhm)
                        };
                        resultList.Add(lotteryData);
                    }
                }
                return resultList;

            }
            return null;
        }

        private string GetLotteryData(dynamic lotteryData)
        {
            var sb = new StringBuilder();
            foreach (var item in lotteryData)
            {
                sb.Append(item.Value + ",");
            }
            return sb.ToString().Remove(sb.Length -1);
        }
    }
}