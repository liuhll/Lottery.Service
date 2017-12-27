using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using ECommon.Components;
using Lottery.WebApi.Result;

namespace Lottery.WebApi.Configration
{
    [Component]
    public class LotteryApiConfiguration : ILotteryApiConfiguration
    {
        public LotteryApiConfiguration()
        {
            HttpConfiguration = GlobalConfiguration.Configuration;
            SetNoCacheForAjaxResponses = true;
            SetNoCacheForAllResponses = true;
        }

        public WrapResultAttribute DefaultWrapResultAttribute => new WrapResultAttribute();

        public bool SetDefaultWrapResult {
            get
            {
                try
                {
                    return Convert.ToBoolean(ConfigurationManager.AppSettings["DefaultWrapResult"]);

                }
                catch (Exception e)
                {
                    return false;
                }       
            }
        }

        public bool SetNoCacheForAjaxResponses { get; set; }

        public bool SetNoCacheForAllResponses { get; set; }

        public List<string> ResultWrappingIgnoreUrls {
            get
            {
                var ignoreUrls = new List<string> {"/swagger"};
                return ignoreUrls;
            }
        }

        public HttpConfiguration HttpConfiguration { get; set; }
    }
}