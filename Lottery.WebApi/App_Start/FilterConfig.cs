using System.Web.Http;
using Lottery.WebApi.Authorization;
using Lottery.WebApi.Filter;

namespace Lottery.WebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(HttpConfiguration config)
        {
            config.Filters.Add(new System.Web.Http.AuthorizeAttribute());         
            config.Filters.Add(new LotteryApiExceptionFilterAttribute());   
            config.Filters.Add(new LotteryApiAuthorizationFilter());
        }
    }
}
