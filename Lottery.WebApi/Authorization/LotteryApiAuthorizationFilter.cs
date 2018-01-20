using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Lottery.WebApi.Authorization
{
    public class LotteryApiAuthorizationFilter : IAuthorizationFilter
    {
        public bool AllowMultiple { get; }

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            throw new NotImplementedException();
        }
    }
}