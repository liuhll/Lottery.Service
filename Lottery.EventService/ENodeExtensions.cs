using System.Collections.Generic;
using System.Net;
using System.Reflection;
using ENode.Commanding;
using ENode.Configurations;
using ENode.EQueue;
using EQueue.Clients.Consumers;
using EQueue.Clients.Producers;
using EQueue.Configurations;
using EQueue.Protocols;
using Lottery.Core.Caching;
using Lottery.Infrastructure;


namespace Lottery.EventService
{
    public static class ENodeExtensions
    {
        private static CommandService _commandService;
        private static DomainEventConsumer _eventConsumer;

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

            _commandService =new CommandService();
            configuration.SetDefault<ICommandService, CommandService>(_commandService);

            return enodeConfiguration;
        }

        public static ENodeConfiguration StartEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _commandService.Initialize(setting: new ProducerSetting
            {
                NameServerList = ServiceConfigSettings.NameServerEndpoints
            });

            _eventConsumer = new DomainEventConsumer().Initialize(setting:new ConsumerSetting()
            {
                NameServerList = ServiceConfigSettings.NameServerEndpoints,
                ConsumeFromWhere = ConsumeFromWhere.LastOffset,
                MessageHandleMode = MessageHandleMode.Parallel,
            });

            _eventConsumer
                .Subscribe(EQueueTopics.LotteryEventTopic)
                .Subscribe(EQueueTopics.LotteryAccountEventTopic)
                .Subscribe(EQueueTopics.UserInfoEventTopic);

            _commandService.Start();
            _eventConsumer.Start();

            return enodeConfiguration;
        }

        public static ENodeConfiguration UseRedisCache(this ENodeConfiguration enodeConfiguration)
        {
            var configuration = enodeConfiguration.GetCommonConfiguration();
            configuration.SetDefault<ICacheManager, RedisCacheManager>(
                new RedisCacheManager(new RedisConnectionWrapper(DataConfigSettings.RedisServiceAddress)));
            return enodeConfiguration;
        }

        public static ENodeConfiguration ShutdownEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _eventConsumer.Shutdown();
            _commandService.Shutdown();

            return enodeConfiguration;
        }
    }
}