using System;
using FluentScheduler;
using Lottery.Crawler;

namespace Lottery.RunApp.Jobs
{
    public class BjpksJob : RunLotteryAbstractJob
    {
   
        public BjpksJob()
        {
        }

        protected override void PreInitialize()
        {
            _lotteryCode = "BJPKS";
        }

        protected override void PostinItialize()
        {
            
        }
    }
}