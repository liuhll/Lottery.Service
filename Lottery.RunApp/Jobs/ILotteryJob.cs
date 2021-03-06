﻿using FluentScheduler;
using Lottery.RunApp.Events;
using System;

namespace Lottery.RunApp.Jobs
{
    public interface ILotteryJob : IJob
    {
        DateTime LastStart { get; }

        DateTime LastEnd { get; }

        bool StopOnError { get; }

        event EventHandler<LotteryJobEventArgs> EachTaskExcuteAfterHandler;
    }
}