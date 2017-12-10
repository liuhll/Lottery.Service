using System.Reflection;
using ECommon.Components;
using ECommon.Configurations;
using ECommon.Logging;
using ENode.Configurations;
using Lottery.Infrastructure;
using Lottery.RunApp.Services;

namespace Lottery.RunApp
{
    public class Bootstrap
    {
        public static void InitializeFramework()
        {
            ServiceConfigSettings.Initialize();
            DataConfigSettings.Initialize();
            InitializeENodeFramework();
        }

        public static void InitializePredictTable()
        {
            var lotteryPredictTableService = ObjectContainer.Resolve<ILotteryPredictTableService>();

            lotteryPredictTableService.InitLotteryPredictTables();
        }

        private static void InitializeENodeFramework()
        {
            var assemblies = new[]
            {
                Assembly.Load("Lottery.Infrastructure"),
                Assembly.Load("Lottery.Commands"),
                Assembly.Load("Lottery.QueryServices"),
                Assembly.Load("Lottery.QueryServices.Dapper"),
                Assembly.Load("Lottery.RunApp")
            };

            ECommon.Configurations.Configuration
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
                .SetUpDataUpdateItems()
                .InitLotteryEngine()
                .StartEQueue()
                .Start();

            ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(Program)).Info("ENode initialized.");
        }
    }
}