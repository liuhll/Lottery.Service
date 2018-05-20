using System;
using System.Collections.Generic;
using EasyHttp.Http;
using ECommon.Extensions;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Extensions;


namespace Lottery.Crawler.Bjpks
{
    public class BjpksDataUpdateItem5 : BaseDataUpdateItem
    {
        public BjpksDataUpdateItem5(DataSiteDto dataSite) : base(dataSite)
        {
        }

        protected override IList<LotteryDataDto> RequestDatas(int finalData)
        {
            _httpClient.Request.Accept = HttpContentTypes.ApplicationJson;
            _httpClient.Request.UserAgent = CHROME_UA;
            var response = _httpClient.Get(_dataSite.Url);
            string lData = response.DynamicBody;
            if (lData == null)
            {
                return null;
            }
            var crawlData = lData.ToObject();
            if (crawlData.errorCode == 0)
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
                            Data = GetLotteryData(data.preDrawCode)
                        };
                        resultList.Add(lotteryData);
                    }

                }

                return resultList;
            }
            return null;
        }

        private string GetLotteryData(string sopencode)
        {
            var datas = sopencode.Split(',');
            var newDatas = new List<string>();
            datas.ForEach(item =>
            {
                item = item.TrimStart('0');
                newDatas.Add(item);
            });
            return newDatas.ToSplitString(",");
        }
    }
}
