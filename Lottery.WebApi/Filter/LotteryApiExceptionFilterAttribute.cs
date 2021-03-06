﻿using ECommon.Components;
using ECommon.Logging;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.Extensions;
using Lottery.Infrastructure.RunTime.Session;
using Lottery.WebApi.Configration;
using Lottery.WebApi.Helper;
using Lottery.WebApi.Result.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using ILoggerFactory = ECommon.Logging.ILoggerFactory;

namespace Lottery.WebApi.Filter
{
    public class LotteryApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;
        private readonly ILotteryApiConfiguration _lotteryApiConfiguration;
        private readonly ILotterySession _lotterySession;

        public LotteryApiExceptionFilterAttribute()
        {
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create("LotteryApi");
            _lotteryApiConfiguration = ObjectContainer.Resolve<ILotteryApiConfiguration>();
            _lotterySession = NullLotterySession.Instance;
        }

        public override void OnException(HttpActionExecutedContext context)
        {
            var wrapResultAttribute = HttpActionDescriptorHelper
                                          .GetWrapResultAttributeOrNull(context.ActionContext.ActionDescriptor) ??
                                      _lotteryApiConfiguration.DefaultWrapResultAttribute;

            if (wrapResultAttribute.LogError)
            {
                _logger.Error(context.Exception);
            }

            if (!wrapResultAttribute.WrapOnError)
            {
                return;
            }

            if (IsIgnoredUrl(context.Request.RequestUri))
            {
                return;
            }
            if (context.Exception is HttpException)
            {
                var httpException = context.Exception as HttpException;
                var httpStatusCode = (HttpStatusCode)httpException.GetHttpCode();

                context.Response = context.Request.CreateResponse(
                    httpStatusCode,
                    new ResponseMessage(
                        new ErrorInfo(GetErrorCode(context), httpException.Message),
                        httpStatusCode == HttpStatusCode.Unauthorized || httpStatusCode == HttpStatusCode.Forbidden
                    )
                );
            }
            else
            {
                context.Response = context.Request.CreateResponse(
                    GetStatusCode(context),
                    new ResponseMessage(
                        new ErrorInfo(GetErrorCode(context), context.Exception.Message),
                        context.Exception is LotteryAuthorizationException)
                );
            }
        }

        private int GetErrorCode(HttpActionExecutedContext context)
        {
            if (context.Exception is LotteryException)
            {
                return ((LotteryException)context.Exception).ErrorCode;
            }
            return ErrorCode.UnknownError;
        }

        private HttpStatusCode GetStatusCode(HttpActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                return HttpStatusCode.OK;
            }

            return HttpStatusCode.InternalServerError;
        }

        protected virtual bool IsIgnoredUrl(Uri uri)
        {
            if (uri == null || uri.AbsolutePath.IsNullOrEmpty())
            {
                return false;
            }

            return _lotteryApiConfiguration.ResultWrappingIgnoreUrls.Any(url => uri.AbsolutePath.StartsWith(url));
        }
    }
}