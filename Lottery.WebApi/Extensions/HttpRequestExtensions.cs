using System.Net.Http;

namespace Lottery.WebApi.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string GetAudience(this HttpRequestMessage request)
        {
            var host = request.RequestUri.Host;
            return host.ToLower();
        }

        public static string GetIssuer(this HttpRequestMessage request)
        {
            var host = request.RequestUri.Host;
            return host.ToLower();
        }
    }
}