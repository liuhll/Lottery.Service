using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lottery.WebApi.Result;

namespace Lottery.WebApi.Configration
{
    public interface ILotteryApiConfiguration
    {
        WrapResultAttribute DefaultWrapResultAttribute { get; }

        bool SetDefaultWrapResult { get; }

        bool SetNoCacheForAjaxResponses { get; set; }

        bool SetNoCacheForAllResponses { get; set; }

        bool SetCamelCaseForAllResponses { get; }

        bool ClearHistroyCache { get; }

        List<string> ResultWrappingIgnoreUrls { get; }

        HttpConfiguration HttpConfiguration { get; set; }
    }
}