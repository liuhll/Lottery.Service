using System.Reflection;
using ECommon.Components;
using ECommon.Configurations;
using ECommon.Logging;
using ENode.Commanding;
using ENode.Configurations;
using ENode.Infrastructure;
using ENode.SqlServer;
using Lottery.Core.Domain.LotteryDatas;
using Lottery.Infrastructure;

namespace Lottery.Tests
{
    public abstract class TestBase
    {
        private static ENodeConfiguration _enodeConfiguration;
        protected static ICommandService _commandService;
        protected static ILogger _logger;

        protected static void Initialize()
        {
            if (_enodeConfiguration != null)
            {
                CleanupEnode();
            }

            ServiceConfigSettings.Initialize();
            DataConfigSettings.Initialize();

            InitializeENode();

            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(TestBase));
            _logger.Info("ENode initialized.");

            _commandService = ObjectContainer.Resolve<ICommandService>();

            ObjectContainer.Resolve<ILockService>().AddLockKey(typeof(LotteryData).Name);

        }

        protected CommandResult ExecuteCommand(ICommand command)
        {
            return _commandService.Execute(command, CommandReturnType.EventHandled,550000);
        }

        protected void SendCommand(ICommand command)
        {
            _commandService.Send(command);
        }

        private static void InitializeENode()
        {
            var assemblies = new[]
            {
                Assembly.Load("Lottery.Infrastructure"),
                Assembly.Load("Lottery.Commands"),                
                Assembly.Load("Lottery.CommandHandlers"),
                Assembly.Load("Lottery.Core"),
                Assembly.Load("Lottery.Denormalizers.Dapper"),
                Assembly.Load("Lottery.ProcessManagers"),
                Assembly.Load("Lottery.QueryServices"),
                Assembly.Load("Lottery.QueryServices.Dapper"),
                Assembly.Load("Lottery.Crawler"),
                Assembly.Load("Lottery.Engine"),
                Assembly.Load("Lottery.Dtos"), 
                Assembly.Load("Lottery.RunApp"), 
                Assembly.Load("Lottery.AppService"), 
                Assembly.Load("Lottery.Tests")
            };
            //var setting = new ConfigurationSetting(DataConfigSettings.ENodeConnectionString);

            _enodeConfiguration = Configuration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseLog4Net()
                .UseJsonNet()
                .RegisterUnhandledExceptionHandler()
                .CreateENode()
                .RegisterENodeComponents()
                .UseSqlServerEventStore()
                .UseSqlServerPublishedVersionStore()
                .UseSqlServerLockService()
                .RegisterBusinessComponents(assemblies)                
                .UseEQueue()
                .UseRedisCache()
                .BuildContainer()
                .InitializeSqlServerEventStore(DataConfigSettings.ENodeConnectionString)
                .InitializeSqlServerPublishedVersionStore(DataConfigSettings.ENodeConnectionString)
                .InitializeSqlServerLockService(DataConfigSettings.ENodeConnectionString)
                .InitializeBusinessAssemblies(assemblies)              
                .SetUpDataUpdateItems()
                .InitLotteryEngine()
                .StartEQueue()
                .Start();
        }

        private static void CleanupEnode()
        {
            _enodeConfiguration.ShutdownEQueue();
            _enodeConfiguration.Stop();
            _logger.Info("ENode shutdown.");

        }
    }
}