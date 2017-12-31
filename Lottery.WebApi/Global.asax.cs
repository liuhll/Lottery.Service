using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using ECommon.Autofac;
using ECommon.Components;
using ECommon.Configurations;
using ECommon.Logging;
using ENode.Configurations;
using Lottery.Infrastructure;
using Lottery.WebApi.Extensions;

namespace Lottery.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private ILogger _logger;
  
        protected void Application_Start()
        {
            InitializeENode();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(FilterConfig.RegisterGlobalFilters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            RegisterControllers();

        }

        private void InitializeENode()
        {
            ServiceConfigSettings.Initialize();
            DataConfigSettings.Initialize();


            var assemblies = new[]
            {
                Assembly.Load("Lottery.Infrastructure"),
                Assembly.Load("Lottery.Commands"),
                Assembly.Load("Lottery.QueryServices"),
                Assembly.Load("Lottery.QueryServices.Dapper"),
                Assembly.Load("Lottery.AppService"),
                Assembly.Load("Lottery.WebApi")
            };

            Configuration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseLog4Net()
                .UseJsonNet()
                .RegisterUnhandledExceptionHandler()
                .CreateENode()
                .RegisterENodeComponents()
                .RegisterBusinessComponents(assemblies)
                .UseEQueue()
                .UseRedisCache()
                .BuildContainer()
                .InitializeBusinessAssemblies(assemblies)
                .InitLotteryEngine()
                .StartEQueue()
                .Start(); 
           
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(GetType());
            _logger.Info("ENode initialized.");
        }

        private void RegisterControllers()
        {
            var webAssembly = Assembly.GetExecutingAssembly();
            var container = (ObjectContainer.Current as AutofacObjectContainer).Container;
            var builder = new ContainerBuilder();
            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(webAssembly);

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // OPTIONAL: Register the Autofac model binder provider.
            builder.RegisterWebApiModelBinderProvider();

            builder.Update(container);

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

       
    }
}
