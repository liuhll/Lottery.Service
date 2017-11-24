using ECommon.Components;
using ECommon.Logging;
using log4net;
using Topshelf;

namespace Lottery.BrokerService
{
    public class BrokerCrier : ServiceControl
    {
        private readonly ILogger _logger = null;

        public BrokerCrier()
        {
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(Bootstrap).FullName);
        }

        public bool Start(HostControl hostControl)
        {           
            Bootstrap.Start();
            _logger.Info("BrokerService 服务启动成功");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Bootstrap.Stop();
            _logger.Info("BrokerService 服务停止成功");
            return true;
        }
    }
}