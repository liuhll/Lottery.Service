using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Filters;
using Swashbuckle.Swagger;

namespace Lottery.WebApi.Authentication
{
    public class AuthenticationHeaderFilter : IOperationFilter
    {
        private static string[] _whiteList = new string[]{ "login", "createuser", "getalllotteryinfos", "identifycode1" };
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();
            var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline(); //判断是否添加权限过滤器
            var isAuthorized = filterPipeline.Select(filterInfo => filterInfo.Instance).Any(filter => filter is IAuthorizationFilter); //判断是否允许匿名方法 
           // var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
            var isIgnore = _whiteList.Any( p=> p== apiDescription.ActionDescriptor.ActionName.ToLower());
            if (isAuthorized && !isIgnore)
            {
                operation.parameters.Add(new Parameter { name = "Authorization", @in = "header", description = "Token", required = false, type = "string" });
            }
        }
    }
}