using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using ECommon.Components;
using ECommon.Extensions;
using ECommon.Logging;
using ECommon.Scheduling;
using ENode.Commanding;
using ENode.Configurations;
using ENode.EQueue;
using ENode.Eventing;
using ENode.Infrastructure;
using EQueue.Broker;
using EQueue.Clients.Consumers;
using EQueue.Clients.Producers;
using EQueue.Configurations;
using EQueue.NameServer;
using EQueue.Protocols;
using Lottery.Core.Caching;
using Lottery.Crawler;
using Lottery.Engine;
using Lottery.Infrastructure;

namespace Lottery.Tests
{
    public static class ENodeExtensions
    {
        private static NameServerController _nameServer;
        private static BrokerController _broker;
        private static CommandService _commandService;
        private static CommandConsumer _commandConsumer;
        private static DomainEventPublisher _eventPublisher;
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

            _commandService = new CommandService();
            _eventPublisher = new DomainEventPublisher();

            configuration.SetDefault<ICommandService, CommandService>(_commandService);
            configuration.SetDefault<IMessagePublisher<DomainEventStreamMessage>, DomainEventPublisher>(_eventPublisher);

            return enodeConfiguration;
        }

        public static ENodeConfiguration StartEQueue(this ENodeConfiguration enodeConfiguration)
        {
  
            var nameServerSetting = new NameServerSetting()
            {
                BindingAddress = ServiceConfigSettings.NameServerAddress
            };
            _nameServer = new NameServerController(nameServerSetting);

            var brokerStorePath = @"c:\Lottery\lottery-equeue-store-test";
            if (Directory.Exists(brokerStorePath))
            {
                Directory.Delete(brokerStorePath, true);
            }

            var brokerSetting = new BrokerSetting(false, brokerStorePath)
            {
                NameServerList = ServiceConfigSettings.NameServerEndpoints,
                
            };

            brokerSetting.BrokerInfo.ProducerAddress = ServiceConfigSettings.BrokerProducerServiceAddress;
            brokerSetting.BrokerInfo.ConsumerAddress = ServiceConfigSettings.BrokerConsumerServiceAddress;
            brokerSetting.BrokerInfo.AdminAddress = ServiceConfigSettings.BrokerAdminServiceAddress;
            _broker = BrokerController.Create(brokerSetting);

            _commandService.Initialize(new CommandResultProcessor().Initialize(new IPEndPoint(IPAddress.Loopback, 9000)), new ProducerSetting
            {
                NameServerList = ServiceConfigSettings.NameServerEndpoints,
            });
            _eventPublisher.Initialize(new ProducerSetting
            {
                NameServerList = ServiceConfigSettings.NameServerEndpoints
            });
            _commandConsumer = new CommandConsumer().Initialize(setting: new ConsumerSetting
            {
                NameServerList = ServiceConfigSettings.NameServerEndpoints,
                ConsumeFromWhere = ConsumeFromWhere.LastOffset,
                MessageHandleMode = MessageHandleMode.Sequential,
                IgnoreLastConsumedOffset = true,
                AutoPull = true
            });
            _eventConsumer = new DomainEventConsumer().Initialize(setting: new ConsumerSetting
            {
                NameServerList = ServiceConfigSettings.NameServerEndpoints,
                ConsumeFromWhere = ConsumeFromWhere.LastOffset,
                MessageHandleMode = MessageHandleMode.Sequential,
                IgnoreLastConsumedOffset = true,
                AutoPull = true
            });

            _commandConsumer
                .Subscribe(EQueueTopics.LotteryCommandTopic)
                .Subscribe(EQueueTopics.LotteryAccountCommandTopic)
                .Subscribe(EQueueTopics.NormCommandTopic)
                ;

            _eventConsumer
                .Subscribe(EQueueTopics.LotteryEventTopic)
                .Subscribe(EQueueTopics.LotteryAccountEventTopic)
                .Subscribe(EQueueTopics.NormEventTopic);

            _nameServer.Start();
            _broker.Start();
            _eventConsumer.Start();
            _commandConsumer.Start();
            _eventPublisher.Start();
            _commandService.Start();

            WaitAllConsumerLoadBalanceComplete();

            return enodeConfiguration;
        }
        public static ENodeConfiguration ShutdownEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _commandService.Shutdown();
            _eventPublisher.Shutdown();
            _commandConsumer.Shutdown();
            _eventConsumer.Shutdown();
            _broker.Shutdown();
            _nameServer.Shutdown();
            return enodeConfiguration;
        }

        public static ENodeConfiguration SetUpDataUpdateItems(this ENodeConfiguration enodeConfiguration)
        {
            DataUpdateContext.Initialize();
            return enodeConfiguration;
        }

        public static ENodeConfiguration UseRedisCache(this ENodeConfiguration enodeConfiguration)
        {
            var configuration = enodeConfiguration.GetCommonConfiguration();
            configuration.SetDefault<ICacheManager, RedisCacheManager>(
                new RedisCacheManager(new RedisConnectionWrapper("127.0.0.1:6379")));
            return enodeConfiguration;
        }

        public static ENodeConfiguration InitLotteryEngine(this ENodeConfiguration enodeConfiguration)
        {
            EngineContext.Initialize();
            return enodeConfiguration;
        }

        private static void WaitAllConsumerLoadBalanceComplete()
        {
            var scheduleService = ObjectContainer.Resolve<IScheduleService>();
            var waitHandle = new ManualResetEvent(false);
            var totalCommandTopicCount = ObjectContainer.Resolve<ITopicProvider<ICommand>>().GetAllTopics().Count();
            var totalEventTopicCount = ObjectContainer.Resolve<ITopicProvider<IDomainEvent>>().GetAllTopics().Count();

            var logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(ENodeExtensions).Name);
            logger.Info("Waiting for all consumer load balance complete, please wait for a moment...");
            scheduleService.StartTask("WaitAllConsumerLoadBalanceComplete", () =>
            {
                var eventConsumerAllocatedQueues = _eventConsumer.Consumer.GetCurrentQueues();
                var commandConsumerAllocatedQueues = _commandConsumer.Consumer.GetCurrentQueues();
                if (eventConsumerAllocatedQueues.Count() == totalEventTopicCount * _broker.Setting.TopicDefaultQueueCount
                    && commandConsumerAllocatedQueues.Count() == totalCommandTopicCount * _broker.Setting.TopicDefaultQueueCount)
                {
                    waitHandle.Set();
                }
            }, 1000, 1000);

            waitHandle.WaitOne();
            scheduleService.StopTask("WaitAllConsumerLoadBalanceComplete");
            logger.Info("All consumer load balance completed.");
        }
    }
}