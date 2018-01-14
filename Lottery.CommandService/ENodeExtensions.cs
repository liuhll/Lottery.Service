using System.Reflection;
using ENode.Configurations;
using ENode.EQueue;
using ENode.Eventing;
using ENode.Infrastructure;
using EQueue.Clients.Consumers;
using EQueue.Clients.Producers;
using EQueue.Configurations;
using EQueue.Protocols;
using Lottery.Core.Caching;
using Lottery.Infrastructure;

namespace Lottery.CommandService
{
    public static class ENodeExtensions
    {
        private static CommandConsumer _commandConsumer;
        private static DomainEventPublisher _eventPublisher;

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

            _eventPublisher = new DomainEventPublisher();
            configuration.SetDefault<IMessagePublisher<DomainEventStreamMessage>, DomainEventPublisher>(_eventPublisher);
            return enodeConfiguration;
        }

        public static ENodeConfiguration StartEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _eventPublisher.Initialize(new ProducerSetting()
            {
                NameServerList = ServiceConfigSettings.NameServerEndpoints
            });

            _commandConsumer = new CommandConsumer().Initialize(setting:new ConsumerSetting()
            {
                NameServerList = ServiceConfigSettings.NameServerEndpoints,
                ConsumeFromWhere = ConsumeFromWhere.LastOffset,
                MessageHandleMode = MessageHandleMode.Parallel,

            });

            _commandConsumer
                .Subscribe(EQueueTopics.LotteryCommandTopic)
                .Subscribe(EQueueTopics.LotteryAccountCommandTopic)
                .Subscribe(EQueueTopics.LotteryProcessManagerTopic)
                .Subscribe(EQueueTopics.UserInfoCommandTopic)
                .Subscribe(EQueueTopics.NormCommandTopic);

            _commandConsumer.Start();
            _eventPublisher.Start();

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
            _commandConsumer.Shutdown();
            _eventPublisher.Shutdown();

            return enodeConfiguration;
        }
    }
}