using System;
using System.Collections.Generic;
using EasyHttp.Http;
using ECommon.Extensions;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Extensions;

namespace Lottery.Crawler.Bjpks
{
    public class BjpksDataUpdateItem4 : BaseDataUpdateItem
    {
        public BjpksDataUpdateItem4(DataSiteDto dataSite) : base(dataSite)
        {
        }

        protected override IList<LotteryDataDto> RequestDatas(int finalData)
        {
            _httpClient.Request.Accept = HttpContentTypes.ApplicationJson;
            var formData = new Dictionary<string, object>()
            {
                { "lotid","1028" },
                { "size","500"},
                { "time",DateTime.Now.ToString("yyyy-MM-dd")}
            };
            var response = _httpClient.Post(_dataSite.Url, formData,null);
            var crawlResult = response.RawText.ToObject();
            string success = crawlResult.success.ToString();
            if (success.ToUpper() == "Y")
            {
                var resultList = new List<LotteryDataDto>();
                foreach (var item in crawlResult.content)
                {
                    var period = Convert.ToInt32(item.speriod);
                    if (period > finalData)
                    {
                       
                        var lotteryData = new LotteryDataDto()
                        {
                            LotteryId = _dataSite.LotteryId,
                            Period = period,
                            LotteryTime = Convert.ToDateTime(item.dopen_time),
                            Data = GetLotteryData(item.sopencode.ToString())
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