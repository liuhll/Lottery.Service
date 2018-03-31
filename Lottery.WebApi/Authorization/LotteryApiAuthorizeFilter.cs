using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using ECommon.Components;
using Lottery.AppService.Authorize;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.Tools;

namespace Lottery.WebApi.Authorization
{
    public class LotteryApiAuthorizeFilter : LotteryBaseAuthorizeFilter
    {

        private readonly string env;
        public LotteryApiAuthorizeFilter()
        {
            env = ConfigHelper.Value("env");
        }


        public override async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
                actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return await continuation();
            }
            try
            {
                var authorizationHelper = ObjectContainer.Resolve<IAuthorizationHelper>();
                var lotteryApiAuthrizeAttributes = GetLotteryApiAuthrizeAttributes(actionContext);
                if (lotteryApiAuthrizeAttributes.Any())
                {
                    await authorizationHelper.AuthorizeAsync(lotteryApiAuthrizeAttributes);
                    return await continuation();
                }
                var apiPath = $"{actionContext.Request.RequestUri.AbsolutePath}";
                if (actionContext.Request.RequestUri.AbsolutePath.Contains(env))
                {
                    apiPath = apiPath.Replace("/" + env, "");
                }
                await authorizationHelper.AuthorizeAsync(apiPath,
                    actionContext.Request.Method);
                return await continuation();
            }
            catch (LotteryAuthorizationException ex)
            {
                _logger.Error(ex);             
                return CreateUnAuthorizedResponse(actionContext,ex.Message);
            }
        }

        private ICollection<LotteryApiAuthorizeAttribute> GetLotteryApiAuthrizeAttributes(HttpActionContext actionContext)
        {
            var allApiAuthorizeAttributes = new List<LotteryApiAuthorizeAttribute>();
            var lotteryApiAuthenticationAttribute1 = actionContext.ActionDescriptor
                .GetCustomAttributes<LotteryApiAuthorizeAttribute>();

            var lotteryApiAuthenticationAttribute2 = actionContext.ActionDescriptor.ControllerDescriptor
                .GetCustomAttributes<LotteryApiAuthorizeAttribute>();

            allApiAuthorizeAttributes.AddRange(lotteryApiAuthenticationAttribute1);
            allApiAuthorizeAttributes.AddRange(lotteryApiAuthenticationAttribute2);
            return allApiAuthorizeAttributes;

        }
    }
}