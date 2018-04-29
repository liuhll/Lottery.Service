using Lottery.WebApi.Authorization;
using Lottery.WebApi.Filter;
using System.Web.Http;

namespace Lottery.WebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(HttpConfiguration config)
        {
            config.Filters.Add(new System.Web.Http.AuthorizeAttribute());
            config.Filters.Add(new LotteryApiExceptionFilterAttribute());
            config.Filters.Add(new SystemTypeAuthorizationFilter());
            config.Filters.Add(new LotteryApiAuthorizeFilter());
        }
    }
}