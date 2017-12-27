using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using ECommon.Extensions;
using Lottery.Infrastructure.Extensions;
using Lottery.WebApi.Configration;
using Lottery.WebApi.Helper;
using Lottery.WebApi.Result.Models;

namespace Lottery.WebApi.Handlers
{
    public class ResultWrapperHandler : DelegatingHandler
    {
        private readonly ILotteryApiConfiguration _configuration;

        public ResultWrapperHandler(ILotteryApiConfiguration lotteryApiConfiguration)
        {
            _configuration = lotteryApiConfiguration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = await base.SendAsync(request, cancellationToken);
            WrapResultIfNeeded(request, result);
            return result;
        }

        private void WrapResultIfNeeded(HttpRequestMessage request, HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return;
            }

            if (_configuration.SetNoCacheForAllResponses)
            {
                //Based on http://stackoverflow.com/questions/49547/making-sure-a-web-page-is-not-cached-across-all-browsers
                response.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoCache = true,
                    NoStore = true,
                    MaxAge = TimeSpan.Zero,
                    MustRevalidate = true
                };
            }

            var wrapAttr = HttpActionDescriptorHelper.GetWrapResultAttributeOrNull(request.GetActionDescriptor())
                           ?? _configuration.DefaultWrapResultAttribute;

            if (!wrapAttr.WrapOnSuccess)
            {
                return;
            }

            if (IsIgnoredUrl(request.RequestUri))
            {
                return;
            }

            object resultObject;
            if (!response.TryGetContentValue(out resultObject) || resultObject == null)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ObjectContent<ResponseMessage>(
                    new ResponseMessage(), 
                    _configuration.HttpConfiguration.Formatters.JsonFormatter
                );
                return;
            }

            if (resultObject is ResponseMessage)
            {
                return;
            }

            response.Content = new ObjectContent<ResponseMessage>(
                new ResponseMessage(resultObject),
                _configuration.HttpConfiguration.Formatters.JsonFormatter
            );
        }

        private bool IsIgnoredUrl(Uri uri)
        {
            if (uri == null || uri.AbsolutePath.IsNullOrEmpty())
            {
                return false;
            }

            return _configuration.ResultWrappingIgnoreUrls.Any(url => uri.AbsolutePath.StartsWith(url));
        }
    }
}