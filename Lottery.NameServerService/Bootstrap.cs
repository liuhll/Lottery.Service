using System.Linq;
using System.Net;
using ECommon.Configurations;
using EQueue.Configurations;
using EQueue.NameServer;
using Lottery.Infrastructure;
using ECommonConfiguration = ECommon.Configurations.Configuration;

namespace Lottery.NameServerService
{
    public class Bootstrap
    {
        private static ECommonConfiguration _ecommonConfiguration;
        private static NameServerController _nameServer;


        public static void Initialize()
        {
            InitializeEQueue();
        }

        public static void Start()
        {
            _nameServer.Start();
        }
        public static void Stop()
        {
            if (_nameServer != null)
            {
                _nameServer.Shutdown();
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
              var setting = new NameServerSetting()
              {
                 BindingAddress = ServiceConfigSettings.NameServerServerEndpoint
              };

            _nameServer = new NameServerController(setting);

        }



    }
}