using Lottery.AppService.Authorize;
using Lottery.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Lottery.WebApi.Authorization
{
    /// <summary>
    /// 系统类型授权
    /// <remarks>策略:默认用户允许登录App,所有的用户均被授权门户,只有被授权的用户才被允许后台管理系统</remarks>
    /// </summary>
    public class SystemTypeAuthorizationFilter : LotteryBaseAuthorizeFilter
    {
        /// <summary>
        /// 系统客户端类型授权过滤器
        /// </summary>
        public SystemTypeAuthorizationFilter() : base()
        {
        }

        public override async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
                actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return await continuation();
            }
            // 获取所有允许的客户端类型
            var allowedClientTypes = GetAllowedClientTypes(actionContext);
            // 如果未指定，则默认允许所有的客户端
            if (!allowedClientTypes.Any())
            {
                return await continuation();
            }
            // 如果允许了该客户端
            if (allowedClientTypes.Any(p => p == _lotterySession.SystemType))
            {
                return await continuation();
            }
            return CreateUnAuthorizedResponse(actionContext, GetUnAuthorizedErrorMessage(GetUnAuthorizedStatusCode(actionContext), allowedClientTypes));
        }

        protected virtual ICollection<SystemType> GetAllowedClientTypes(HttpActionContext actionContext)
        {
            var allowedClientTypes = new List<SystemType>();
            var lotteryApiAuthenticationAttribute1 = actionContext.ActionDescriptor
                .GetCustomAttributes<SystemTypeAuthorizeAttribute>();

            var lotteryApiAuthenticationAttribute2 = actionContext.ActionDescriptor.ControllerDescriptor
                .GetCustomAttributes<SystemTypeAuthorizeAttribute>();
            foreach (var attribute in lotteryApiAuthenticationAttribute1)
            {
                allowedClientTypes.AddRange(attribute.ClientTypes);
            }
            foreach (var attribute in lotteryApiAuthenticationAttribute2)
            {
                allowedClientTypes.AddRange(attribute.ClientTypes);
            }
            //去重
            allowedClientTypes = allowedClientTypes.Where((x, i) => allowedClientTypes.FindIndex(z => z == x) == i)
                .ToList();
            return allowedClientTypes;
        }
    }
}