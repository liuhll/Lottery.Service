using Lottery.WebApi.Result;
using System.Collections.Generic;
using System.Web.Http;

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