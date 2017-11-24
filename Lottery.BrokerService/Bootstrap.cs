using System.Collections.Generic;
using System.Net;
using ECommon.Components;
using ECommon.Configurations;
using ECommon.Extensions;
using ECommon.Logging;
using EQueue.Broker;
using EQueue.Configurations;
using Lottery.Infrastructure;
using ECommonConfiguration = ECommon.Configurations.Configuration;

namespace Lottery.BrokerService
{
    public class Bootstrap
    {
        private static ECommonConfiguration _ecommonConfiguration;
        private static BrokerController _broker;

        public static void Initialize()
        {
            InitializeEQueue();
        }

        public static void Start()
        {
            _broker.Start();
        }
        public static void Stop()
        {
            if (_broker != null)
            {
                _broker.Shutdown();
            }
        }

        private static void InitializeEQueue()
        {
            _ecommonConfiguration = ECommonConfiguration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseLog4Net()
                .UseJsonNet()
                .RegisterUnhandledExceptionHandler()
                .RegisterEQueueComponents()
                .BuildContainer();

            ConfigSettings.Initialize();
            var nameServerEndpoint = new IPEndPoint(IPAddress.Parse(ConfigSettings.NameServerIp), ConfigSettings.NameServerPort);
            var nameServerEndpoints = new List<IPEndPoint> { nameServerEndpoint };

            var brokerSetting = new BrokerSetting(false, ConfigSettings.EqueueStorePath)
            {
                NameServerList = nameServerEndpoints
            };

            brokerSetting.BrokerInfo.ProducerAddress = new IPEndPoint(IPAddress.Parse(ConfigSettings.BrokerProducerServiceIp), ConfigSettings.BrokerProducerPort).ToAddress();
            brokerSetting.BrokerInfo.ConsumerAddress = new IPEndPoint(IPAddress.Parse(ConfigSettings.BrokerConsumerServiceIp), ConfigSettings.BrokerConsumerPort).ToAddress();
            brokerSetting.BrokerInfo.AdminAddress = new IPEndPoint(IPAddress.Parse(ConfigSettings.BrokerAdminServiceIp), ConfigSettings.BrokerAdminPort).ToAddress();

            _broker = BrokerController.Create(brokerSetting);
            ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(Bootstrap).FullName).Info("Broker initialized.");

        }
    }
}