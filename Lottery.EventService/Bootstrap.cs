using System.Reflection;
using System.Threading;
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
            int minWorker, minIOC;
            // Get the current settings.
            ThreadPool.GetMinThreads(out minWorker, out minIOC);
            // Change the minimum number of worker threads to four, but
            // keep the old setting for minimum asynchronous I/O 
            // completion threads.
            ThreadPool.SetMinThreads(250, minIOC);

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

          _enodeConfiguration = Configuration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseLog4Net()
                .UseJsonNet()
                .RegisterUnhandledExceptionHandler()
                .CreateENode()
                .RegisterENodeComponents()
                .RegisterBusinessComponents(assemblies)
                .UseSqlServerPublishedVersionStore()
                .UseEQueue()
                .UseRedisCache()
                .BuildContainer()
                .InitializeSqlServerPublishedVersionStore(DataConfigSettings.ENodeConnectionString)
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