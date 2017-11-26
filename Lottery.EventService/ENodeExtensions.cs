using System.Reflection;
using ENode.Commanding;
using ENode.Configurations;
using ENode.EQueue;
using EQueue.Clients.Consumers;
using EQueue.Clients.Producers;
using EQueue.Configurations;
using Lottery.Infrastructure;


namespace Lottery.EventService
{
    public static class ENodeExtensions
    {
        //private static ENode.EQueue.CommandService _commandService;
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

            //_commandService = new ENode.EQueue.CommandService();
            //enodeConfiguration.GetCommonConfiguration()
            //    .SetDefault<ICommandService, ENode.EQueue.CommandService>(_commandService);

            return enodeConfiguration;
        }

        public static ENodeConfiguration StartEQueue(this ENodeConfiguration enodeConfiguration)
        {
            //_commandService.Initialize(setting: new ProducerSetting
            //{
            //    NameServerList = ServiceConfigSettings.NameServerEndpoints
            //});

            _eventConsumer = new DomainEventConsumer().Initialize(setting:new ConsumerSetting()
            {
                NameServerList = ServiceConfigSettings.NameServerEndpoints
            });

            _eventConsumer
                .Subscribe(EQueueTopics.RunLotteryEventTopic);

            //_commandService.Start();
            _eventConsumer.Start();

            return enodeConfiguration;
        }

        public static ENodeConfiguration ShutdownEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _eventConsumer.Shutdown();
            //_commandService.Shutdown();

            return enodeConfiguration;
        }
    }
}