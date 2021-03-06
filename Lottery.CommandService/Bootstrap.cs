﻿using ECommon.Components;
using ECommon.Configurations;
using ECommon.Logging;
using ENode.Configurations;
using ENode.Infrastructure;
using ENode.SqlServer;
using Lottery.Core.Domain.LotteryDatas;
using Lottery.Core.Domain.NormConfigs;
using Lottery.Infrastructure;
using System.Reflection;

namespace Lottery.CommandService
{
    public class Bootstrap
    {
        private static ENodeConfiguration _enodeConfiguration;

        public static void Initialize()
        {
            InitializeENode();
            InitializeCommandService();
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
                Assembly.Load("Lottery.CommandHandlers"),
                Assembly.Load("Lottery.CommandService"),
            };
            // var setting = new ConfigurationSetting(DataConfigSettings.ENodeConnectionString);

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
                .UseSqlServerEventStore()
                .UseSqlServerLockService()
                .UseEQueue()
                .UseRedisCache()
                .BuildContainer()
                .InitializeBusinessAssemblies(assemblies)
                .InitializeSqlServerEventStore(DataConfigSettings.ENodeConnectionString)
                .InitializeSqlServerLockService(DataConfigSettings.ENodeConnectionString)
                .Start();
        }

        private static void InitializeCommandService()
        {
            ObjectContainer.Resolve<ILockService>().AddLockKey(typeof(LotteryData).Name);
            ObjectContainer.Resolve<ILockService>().AddLockKey(typeof(NormConfig).Name);
            ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(Program)).Info("Command service initialized.");
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