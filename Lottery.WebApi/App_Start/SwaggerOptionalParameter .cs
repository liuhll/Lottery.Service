using Swashbuckle.Swagger;
using System;
using System.Linq;
using System.Web.Http.Description;

namespace Lottery.WebApi
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SwaggerOptionalParameter : Attribute
    {
        private string _parameters;

        public SwaggerOptionalParameter(string parameters)
        {
            _parameters = parameters;
        }

        public string[] Parameters
        {
            get { return _parameters.Split(','); }
        }
    }

    public class AddSwaggerOptionalParameter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
                return;
            foreach (var param in operation.parameters)
            {
                var actionParam = apiDescription.ActionDescriptor.GetParameters().First(p => p.ParameterName == param.name);

                if (actionParam != null)
                {
                    var customAttribute = actionParam.ActionDescriptor.GetCustomAttributes<SwaggerOptionalParameter>().FirstOrDefault(p => p.Parameters.Contains(param.name));
                    if (customAttribute != null)
                    {
                        param.required = false;
                    }
                }
            }
        }
    }
}