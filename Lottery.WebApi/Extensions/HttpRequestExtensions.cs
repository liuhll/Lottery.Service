using System.Net.Http;

namespace Lottery.WebApi.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string GetAudience(this HttpRequestMessage request)
        {
            var host = $"{ request.RequestUri.Scheme }://{ request.RequestUri.Host }:{request.RequestUri.Port}";
            return host.ToLower();
        }

        public static string GetIssuer(this HttpRequestMessage request)
        {
            var host =$"{ request.RequestUri.Scheme }://{ request.RequestUri.Host }:{request.RequestUri.Port}";
            return host.ToLower();
        }

        public static string GetReuestIp(this HttpRequestMessage request)
        {
            var host = request.RequestUri.Host;
            return host.ToString();
        }
    }
}