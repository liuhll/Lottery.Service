﻿using ECommon.Components;
using ECommon.Logging;
using Topshelf;

namespace Lottery.EventService
{
    public class EventServiceCrier : ServiceControl
    {
        private readonly ILogger _logger = null;

        public EventServiceCrier()
        {
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(Bootstrap).FullName);
        }

        public bool Start(HostControl hostControl)
        {
            Bootstrap.Start();
            _logger.Info("EventService 服务启动成功");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Bootstrap.Stop();
            hostControl.Stop();
            _logger.Info("EventService 服务停止成功");
            return true;
        }
    }
}