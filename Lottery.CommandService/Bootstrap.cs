﻿using System.Reflection;
using ECommon.Components;
using ECommon.Configurations;
using ECommon.Logging;
using ENode.Configurations;
using ENode.Infrastructure;
using ENode.SqlServer;
using EQueue.Configurations;
using Lottery.Core.Domain.UserInfos;
using Lottery.Infrastructure;

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
                .UseSqlServerEventStore()
                .UseSqlServerLockService()
                .UseEQueue()
                .BuildContainer()
                .InitializeSqlServerEventStore()
                .InitializeSqlServerLockService()
                .RegisterBusinessComponents(assemblies);

        }

        private static void InitializeCommandService()
        {
            ObjectContainer.Resolve<ILockService>().AddLockKey(typeof(UserInfo).Name);
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