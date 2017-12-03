using System.Reflection;
using ECommon.Components;
using ECommon.Configurations;
using ECommon.Logging;
using ENode.Configurations;
using ENode.SqlServer;
using Lottery.Infrastructure;

namespace Lottery.EventService
{
    public class Bootstrap
    {
        private static ENodeConfiguration _enodeConfiguration;

        public static void Initialize()
        {
            InitializeENode();
        }

        private static void InitializeENode()
        {
            ServiceConfigSettings.Initialize();
            DataConfigSettings.Initialize();

            var assemblies = new[]
            {
                Assembly.Load("Lottery.Infrastructure"),
                Assembly.Load("Lottery.Commands"),
                Assembly.Load("Lottery.Core"),
                Assembly.Load("Lottery.Denormalizers.Dapper"),
                Assembly.Load("Lottery.ProcessManagers"),
                Assembly.Load("Lottery.EventService")
            };

            var setting = new ConfigurationSetting(DataConfigSettings.ENodeConnectionString);

            _enodeConfiguration = Configuration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseLog4Net()
                .UseJsonNet()
                .RegisterUnhandledExceptionHandler()
                .CreateENode(setting)
                .RegisterENodeComponents()
                .RegisterBusinessComponents(assemblies)
                .UseSqlServerPublishedVersionStore()
                .UseEQueue()
                .UseRedisCache()
                .BuildContainer()
                .InitializeSqlServerPublishedVersionStore()
                .InitializeBusinessAssemblies(assemblies);

            ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(Program)).Info("Event service initialized.");

        }

        public static void Start()
        {
            _enodeConfiguration.StartEQueue();
        }
        public static void Stop()
        {
            _enodeConfiguration.ShutdownEQueue();
        }
    }
}