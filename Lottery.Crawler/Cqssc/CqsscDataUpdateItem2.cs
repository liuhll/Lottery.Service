﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using EasyHttp.Http;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure.Extensions;

namespace Lottery.Crawler.Cqssc
{
    public class CqsscDataUpdateItem2 : BaseDataUpdateItem
    {
        private readonly string _site;
        public CqsscDataUpdateItem2(DataSiteDto dataSite) : base(dataSite)
        {
            _site = string.Format(dataSite.Url, DateTime.Now.ToString("yyyyMMdd"));
        }

        protected override IList<LotteryDataDto> RequestDatas(int finalData)
        {
            _httpClient.Request.Accept = HttpContentTypes.ApplicationXml;
            _httpClient.Request.UserAgent = CHROME_UA;
            var response = _httpClient.Get(_site);
            var crawlData = response.RawText;
            crawlData = crawlData.Replace("gb2312", "utf-8");
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(crawlData);

            var lotteryDatas = xmlDoc.SelectNodes("/xml/row");
            if (lotteryDatas != null)
            {
                var resultList = new List<LotteryDataDto>();
                foreach (XmlNode item in lotteryDatas)
                {
                    var periodStr = item.Attributes["expect"].Value.Substring(2);
                    var period = Convert.ToInt32(periodStr);
                    if (period > finalData)
                    {
                        var lotteryData = new LotteryDataDto()
                        {
                            LotteryId = _dataSite.LotteryId,
                            Period = period,
                            LotteryTime = Convert.ToDateTime(item.Attributes["opentime"].Value),
                            Data = item.Attributes["opencode"].Value
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