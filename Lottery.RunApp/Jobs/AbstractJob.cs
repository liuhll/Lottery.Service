using FluentScheduler;
using System;

namespace Lottery.RunApp.Jobs
{
    public abstract class AbstractJob : IJob
    {
        public DateTime LastStart { get; private set; }

        public DateTime LastEnd { get; private set; }

        public bool StopOnError { get; private set; }

        public abstract void Execute();
    }
}