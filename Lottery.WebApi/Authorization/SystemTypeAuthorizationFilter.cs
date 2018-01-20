using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ECommon.Components;
using ECommon.Logging;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Extensions;
using Lottery.WebApi.Authentication;
using Lottery.WebApi.Configration;
using Lottery.WebApi.Helper;
using Lottery.WebApi.Result.Models;
using Lottery.WebApi.RunTime.Session;

namespace Lottery.WebApi.Authorization
{
    /// <summary>
    /// 系统类型授权
    /// <remarks>策略:默认用户允许登录App,所有的用户均被授权门户,只有被授权的用户才被允许后台管理系统</remarks>
    /// </summary>
    public class SystemTypeAuthorizationFilter : IAuthorizationFilter
    {
        private readonly ILogger _logger;
        private readonly ILotterySession _lotterySession;
        private readonly ILotteryApiConfiguration _lotteryApiConfiguration;
        public SystemTypeAuthorizationFilter()
        {
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create("LotteryApi");
            _lotteryApiConfiguration = ObjectContainer.Resolve<ILotteryApiConfiguration>();
            _lotterySession = NullLotterySession.Instance;
        }

        public bool AllowMultiple { get; }

        public virtual async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken,
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
            if (allowedClientTypes.Any(p=> p == _lotterySession.SystemType))
            {
                return await continuation();
            }
            return CreateUnAuthorizedResponse(actionContext, allowedClientTypes);
        }

        private ICollection<SystemType> GetAllowedClientTypes(HttpActionContext actionContext)
        {
            var allowedClientTypes = new List<SystemType>();
            var lotteryApiAuthenticationAttribute1 = actionContext.ActionDescriptor
                .GetCustomAttributes<LotteryApiAuthenticationAttribute>();

            var lotteryApiAuthenticationAttribute2 = actionContext.ActionDescriptor.ControllerDescriptor
                .GetCustomAttributes<LotteryApiAuthenticationAttribute>();
            foreach (var attribute in lotteryApiAuthenticationAttribute1)
            {
                allowedClientTypes.AddRange(attribute.ClientType);
            }
            foreach (var attribute in lotteryApiAuthenticationAttribute2)
            {
                allowedClientTypes.AddRange(attribute.ClientType);
            }
            //去重
            allowedClientTypes = allowedClientTypes.Where((x, i) => allowedClientTypes.FindIndex(z => z == x) == i)
                .ToList();
            return allowedClientTypes;
        }

        protected virtual HttpResponseMessage CreateUnAuthorizedResponse(HttpActionContext actionContext, ICollection<SystemType> allowedClientTypes)
        {
            var statusCode = GetUnAuthorizedStatusCode(actionContext);

            var wrapResultAttribute =
                HttpActionDescriptorHelper.GetWrapResultAttributeOrNull(actionContext.ActionDescriptor) ??
                _lotteryApiConfiguration.DefaultWrapResultAttribute;

            if (!wrapResultAttribute.WrapOnError)
            {
                return new HttpResponseMessage(statusCode);
            }

            return new HttpResponseMessage(statusCode)
            {
                Content = new ObjectContent<ResponseMessage>(
                    new ResponseMessage(new ErrorInfo(
                        GetUnAuthorizedErrorMessage(statusCode, allowedClientTypes)

                    ), statusCode == HttpStatusCode.Unauthorized || statusCode == HttpStatusCode.Forbidden), 
                    _lotteryApiConfiguration.HttpConfiguration.Formatters.JsonFormatter
                )
            };
        }

        private string GetUnAuthorizedErrorMessage(HttpStatusCode statusCode, ICollection<SystemType> clientTypes)
        {
            var clientTypeStr = "";
            foreach (var clientType in clientTypes)
            {
                clientTypeStr += clientType.GetChineseDescribe() + ",";
            }
            clientTypeStr = clientTypeStr.Remove(clientTypeStr.Length -1 , 1);
            if (statusCode == HttpStatusCode.Forbidden)
            {
                return $"用户未被授权访问{clientTypeStr}客户端";
            }
            return "未认证";
        }

        private static HttpStatusCode GetUnAuthorizedStatusCode(HttpActionContext actionContext)
        {
            return (actionContext.RequestContext.Principal?.Identity?.IsAuthenticated ?? false)
                ? HttpStatusCode.Forbidden
                : HttpStatusCode.Unauthorized;
        }

        
        
    }
}