using ECommon.Components;
using ECommon.Logging;
using Topshelf;

namespace Lottery.NameServerService
{
    public class NameServerCrier : ServiceControl
    {
        private readonly ILogger _logger = null;

        public NameServerCrier()
        {
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(Bootstrap).FullName);
        }

        public bool Start(HostControl hostControl)
        {
            Bootstrap.Start();
            _logger.Info("NameServer 服务启动成功");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Bootstrap.Stop();
            _logger.Info("NameServer 服务停止成功");
            return true;
        }
    }
}