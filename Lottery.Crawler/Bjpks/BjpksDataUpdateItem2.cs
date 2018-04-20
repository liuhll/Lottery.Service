using EasyHttp.Http;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lottery.Crawler.Bjpks
{
    public class BjpksDataUpdateItem2 : BaseDataUpdateItem
    {
        public BjpksDataUpdateItem2(DataSiteDto dataSite) : base(dataSite)
        {
        }

        protected override IList<LotteryDataDto> RequestDatas(int finalData)
        {
            _httpClient.Request.Accept = HttpContentTypes.ApplicationJson;
            var response = _httpClient.Post(_dataSite.Url, new { limit = _dataSite.Count }, contentType: "application/json");
            var crawlResult = response.DynamicBody;
            if (crawlResult.status == 1)
            {
                var resultDatas = new List<LotteryDataDto>(_dataSite.Count);
                var requestDatas = crawlResult.data;
                var count = 1;
                foreach (var item in requestDatas)
                {
                    var period = Convert.ToInt32(item.issue);
                    if (period <= finalData)
                    {
                        continue;
                    }
                    var data = new LotteryDataDto()
                    {
                        Data = GetLotteryData(item.kjhm),
                        LotteryId = _dataSite.LotteryId,
                        LotteryTime = DateTimeExtensions.TimeStampConvetDateTime(Convert.ToInt64(item.kjtime)),
                        Period = period
                    };
                    resultDatas.Add(data);
                    count++;
                    //if (count > _dataSite.Count)
                    //{
                    //    break;
                    //}
                }

                return resultDatas;
            }
            return null;
        }

        private string GetLotteryData(dynamic kjhm)
        {
            var lotteryData = string.Empty;
            foreach (var item in kjhm)
            {
                lotteryData += item.Value + ",";
            }
            Debug.Assert(!string.IsNullOrEmpty(lotteryData), "!string.IsNullOrEmpty(lotteryData)");
            lotteryData = lotteryData.Remove(lotteryData.Length - 1);
            return lotteryData;
        }
    }
}