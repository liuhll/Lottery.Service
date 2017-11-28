using System;
using FluentScheduler;

namespace Lottery.RunApp.Jobs
{
    public class BjpksJob : RunLotteryAbstractJob
    {

        public BjpksJob()
        {

        }

        public override void Execute()
        {
            Console.WriteLine(_timeRuleManager.IsLotteryDuration);
        }

        protected override void PreinItialize()
        {
            _lotteryCode = "BJPKS";
        }
    }
}