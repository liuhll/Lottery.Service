﻿using ECommon.Components;
using ENode.Commanding;
using ENode.Configurations;
using ENode.EQueue;
using EQueue.Clients.Producers;
using EQueue.Configurations;
using Lottery.Core.Caching;
using Lottery.Crawler;
using Lottery.Engine;
using Lottery.Infrastructure;
using System.Reflection;

namespace Lottery.RunApp
{
    public static class ENodeExtensions
    {
        private static CommandService _commandService;

        public static ENodeConfiguration BuildContainer(this ENodeConfiguration enodeConfiguration)
        {
            enodeConfiguration.GetCommonConfiguration().BuildContainer();
            return enodeConfiguration;
        }

        public static ENodeConfiguration UseEQueue(this ENodeConfiguration enodeConfiguration)
        {
            var assemblies = new[] { Assembly.GetExecutingAssembly() };
            enodeConfiguration.RegisterTopicProviders(assemblies);

            var configuration = enodeConfiguration.GetCommonConfiguration();
            configuration.RegisterEQueueComponents();

            _commandService = new CommandService();
            configuration.SetDefault<ICommandService, CommandService>(_commandService);

            return enodeConfiguration;
        }

        public static ENodeConfiguration StartEQueue(this ENodeConfiguration enodeConfiguration)
        {
            var commandResultProcessor = new CommandResultProcessor().Initialize(ServiceConfigSettings.CommandServiceprocessorAddress);

            _commandService.Initialize(commandResultProcessor, new ProducerSetting
            {
                NameServerList = ServiceConfigSettings.NameServerEndpoints
            });

            _commandService.Start();
            return enodeConfiguration;
        }

        public static ENodeConfiguration ShutdownEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _commandService.Start();
            return enodeConfiguration;
        }

        public static ENodeConfiguration UseRedisCache(this ENodeConfiguration enodeConfiguration)
        {
            var configuration = enodeConfiguration.GetCommonConfiguration();
            configuration.SetDefault<ICacheManager, RedisCacheManager>(
                new RedisCacheManager(new RedisConnectionWrapper(DataConfigSettings.RedisServiceAddress)));
            return enodeConfiguration;
        }

        public static ENodeConfiguration ClearCache(this ENodeConfiguration enodeConfiguration)
        {
            var cacheManager = ObjectContainer.Resolve<ICacheManager>();
            cacheManager.Clear();
            return enodeConfiguration;
        }

        public static ENodeConfiguration SetUpDataUpdateItems(this ENodeConfiguration enodeConfiguration)
        {
            DataUpdateContext.Initialize();
            return enodeConfiguration;
        }

        public static ENodeConfiguration InitLotteryEngine(this ENodeConfiguration enodeConfiguration)
        {
            EngineContext.Initialize();
            return enodeConfiguration;
        }
    }
}