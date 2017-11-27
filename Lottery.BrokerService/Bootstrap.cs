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

            ServiceConfigSettings.Initialize();
           
            var brokerSetting = new BrokerSetting(false, ServiceConfigSettings.EqueueStorePath)
            {
                NameServerList = ServiceConfigSettings.NameServerEndpoints
            };

            brokerSetting.BrokerInfo.ProducerAddress = ServiceConfigSettings.BrokerProducerServiceAddress;
            brokerSetting.BrokerInfo.ConsumerAddress = ServiceConfigSettings.BrokerConsumerServiceAddress;
            brokerSetting.BrokerInfo.AdminAddress = ServiceConfigSettings.BrokerAdminServiceAddress;
            brokerSetting.BrokerInfo.BrokerName = ServiceConfigSettings.BrokerName;
            brokerSetting.BrokerInfo.GroupName = ServiceConfigSettings.BrokerGroup;

            _broker = BrokerController.Create(brokerSetting);
            ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(Program).FullName).Info("Broker initialized.");

        }
    }
}