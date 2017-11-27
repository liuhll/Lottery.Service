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
            throw new System.NotImplementedException();
        }

        public bool Stop(HostControl hostControl)
        {
            throw new System.NotImplementedException();
        }
    }
}