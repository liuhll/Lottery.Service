using System;
using FluentScheduler;

namespace Lottery.RunApp.Jobs
{
    public class BjpksJob : RunLotteryAbstractJob
    {
        private int count = 1;
        private int count2 = 1;
        public BjpksJob()
        {
            count += 1;
            count2 += 1;
        }

        public override void Execute()
        {
            Console.WriteLine(_timeRuleManager.IsLotteryDuration);



            Console.WriteLine(_timeRuleManager.TodayTotalCount);

            Console.WriteLine(_timeRuleManager.NextLotteryTime().Value);
            Console.WriteLine();
            count2 += 1;
            Console.WriteLine(count);
            Console.WriteLine(count2);
        }

        protected override void PreinItialize()
        {
            _lotteryCode = "BJPKS";
        }
    }
}