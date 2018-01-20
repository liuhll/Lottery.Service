using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ECommon.Components;
using ECommon.Logging;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Extensions;
using Lottery.Infrastructure.RunTime.Session;
using Lottery.WebApi.Configration;
using Lottery.WebApi.Helper;
using Lottery.WebApi.Result.Models;

namespace Lottery.WebApi.Authorization
{
    public abstract class LotteryBaseAuthorizeFilter : IAuthorizationFilter
    {
        protected readonly ILogger _logger;
        protected readonly ILotterySession _lotterySession;
        protected readonly ILotteryApiConfiguration _lotteryApiConfiguration;

        protected LotteryBaseAuthorizeFilter()
        {
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create("LotteryApi");
            _lotteryApiConfiguration = ObjectContainer.Resolve<ILotteryApiConfiguration>();
            _lotterySession = NullLotterySession.Instance;
        }

        public virtual bool AllowMultiple { get; }

        public abstract Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext,
            CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation);

      
        protected virtual HttpResponseMessage CreateUnAuthorizedResponse(HttpActionContext actionContext, string errorMessage)
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
                    new ResponseMessage(new ErrorInfo(errorMessage), 
                    statusCode == HttpStatusCode.Unauthorized || statusCode == HttpStatusCode.Forbidden),
                    _lotteryApiConfiguration.HttpConfiguration.Formatters.JsonFormatter)

            };
        }

        protected string GetUnAuthorizedErrorMessage(HttpStatusCode statusCode, ICollection<SystemType> clientTypes)
        {
            var clientTypeStr = "";
            foreach (var clientType in clientTypes)
            {
                clientTypeStr += clientType.GetChineseDescribe() + ",";
            }
            clientTypeStr = clientTypeStr.Remove(clientTypeStr.Length - 1, 1);
            if (statusCode == HttpStatusCode.Forbidden)
            {
                return $"用户未被授权访问{clientTypeStr}客户端";
            }
            return "未认证的请求";
        }

        protected static HttpStatusCode GetUnAuthorizedStatusCode(HttpActionContext actionContext)
        {
            return (actionContext.RequestContext.Principal?.Identity?.IsAuthenticated ?? false)
                ? HttpStatusCode.Forbidden
                : HttpStatusCode.Unauthorized;
        }

    }
}