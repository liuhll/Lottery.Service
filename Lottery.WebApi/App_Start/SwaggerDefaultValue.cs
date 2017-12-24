using System;
using System.Linq;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace Lottery.WebApi
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SwaggerDefaultValue : Attribute
    {
        public string ParameterName { get; set; }
        public string Value { get; set; }

        public SwaggerDefaultValue(string parameterName, string value)
        {
            this.ParameterName = parameterName;
            this.Value = value;
        }
    }

    public class AddDefaultValues : IOperationFilter
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
                    var customAttribute = actionParam.ActionDescriptor.GetCustomAttributes<SwaggerDefaultValue>().FirstOrDefault(p => p.ParameterName == param.name);
                    if (customAttribute != null)
                    {
                        param.@default = customAttribute.Value;
                      
                    }
                }
            }
        }
    }
}