using ECommon.Components;
using Lottery.WebApi.Authentication;
using Lottery.WebApi.Configration;
using Lottery.WebApi.Dynamic;
using Lottery.WebApi.Handlers;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;

namespace Lottery.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // 跨域配置
            config.EnableCors(new EnableCorsAttribute("*", "*", "GET,POST,PUT,DELETE"));
            // Web API 配置和服务
            // 将 Web API 配置为仅使用不记名令牌身份验证。
            config.Services.Replace(typeof(IHttpActionSelector), new LotteryApiControllerActionSelector(ObjectContainer.Resolve<ILotteryApiConfiguration>()));

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.MessageHandlers.Add(new TokenValidationHandler());
            config.MessageHandlers.Add(new ResultWrapperHandler(ObjectContainer.Resolve<ILotteryApiConfiguration>()));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}