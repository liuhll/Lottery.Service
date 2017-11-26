using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using ECommon.Components;
using ECommon.Logging;
using ECommon.Scheduling;
using ENode.Commanding;
using ENode.Configurations;
using ENode.EQueue;
using ENode.Eventing;
using EQueue.Broker;
using EQueue.Clients.Producers;
using EQueue.Configurations;
using Lottery.Infrastructure;

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

            var commandResultProcessor = new CommandResultProcessor().Initialize(new IPEndPoint(IPAddress.Loopback, 9000));

            _commandService.Initialize(commandResultProcessor, new ProducerSetting
            {
                NameServerList = ServiceConfigSettings.NameServerEndpoints
            });

            _commandService.Start();
            return enodeConfiguration;
        }


    }
}