using ECommon.Components;
using ECommon.Logging;
using System;
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
            try
            {
                Bootstrap.Start();
                _logger.Info("BrokerServer 服务启动成功");
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return false;
            }
        }

        public bool Stop(HostControl hostControl)
        {
            try
            {
                Bootstrap.Stop();
                hostControl.Stop();
                _logger.Info("BrokerServer 服务停止成功");
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return true;
            }
        }
    }
}