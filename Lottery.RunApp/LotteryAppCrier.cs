using ECommon.Components;
using ECommon.Logging;
using Topshelf;

namespace Lottery.RunApp
{
    public class LotteryAppCrier : ServiceControl
    {
        private readonly ILogger _logger = null;

        public LotteryAppCrier()
        {
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(Bootstrap).FullName);
        }

        public bool Start(HostControl hostControl)
        {
            Bootstrap.Start();
            _logger.Info("LotteryApp 服务启动成功");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _logger.Info("LotteryApp 服务停止成功");
            return true;
        }
    }
}