using System;
using ECommon.Components;
using ECommon.Logging;


namespace Lottery.Infrastructure.Logs
{
    public class NullLotteryLogger
    {
        private static ILogger _logger;
        static NullLotteryLogger()
        {
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create("LotteryLogger");
        }

        public static ILogger Instance => _logger;
    }
}
