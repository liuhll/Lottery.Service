using System.Web.Http;
using System.Web.Mvc;

namespace Lottery.WebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(HttpConfiguration config)
        {
            config.Filters.Add(new System.Web.Http.AuthorizeAttribute());
        }
    }
}
