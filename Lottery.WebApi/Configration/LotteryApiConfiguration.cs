using ECommon.Components;
using Lottery.WebApi.Result;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;

namespace Lottery.WebApi.Configration
{
    [Component]
    public class LotteryApiConfiguration : ILotteryApiConfiguration
    {
        private HttpConfiguration _httpConfiguration;

        public LotteryApiConfiguration()
        {
            HttpConfiguration = GlobalConfiguration.Configuration;
            SetNoCacheForAjaxResponses = true;
            SetNoCacheForAllResponses = true;
        }

        public WrapResultAttribute DefaultWrapResultAttribute => new WrapResultAttribute();

        public bool SetDefaultWrapResult
        {
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

        public bool SetCamelCaseForAllResponses
        {
            get
            {
                try
                {
                    return Convert.ToBoolean(ConfigurationManager.AppSettings["ResponseFormatterIsCamelCase"]);
                }
                catch (Exception e)
                {
                    return true;
                }
            }
        }

        public bool ClearHistroyCache
        {
            get
            {
                try
                {
                    return Convert.ToBoolean(ConfigurationManager.AppSettings["ClearHistroyCache"]);
                }
                catch (Exception e)
                {
                    return true;
                }
            }
        }

        public List<string> ResultWrappingIgnoreUrls
        {
            get
            {
                var ignoreUrls = new List<string> { "/swagger" };
                return ignoreUrls;
            }
        }

        public HttpConfiguration HttpConfiguration
        {
            get => _httpConfiguration;
            set
            {
                _httpConfiguration = value;
                if (SetCamelCaseForAllResponses)
                {
                    _httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                }
            }
        }
    }
}