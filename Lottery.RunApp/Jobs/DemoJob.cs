using FluentScheduler;
using System;

namespace Lottery.RunApp.Jobs
{
    public class DemoJob : IJob
    {
        public void Execute()
        {
            Console.WriteLine("this is a demo" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}