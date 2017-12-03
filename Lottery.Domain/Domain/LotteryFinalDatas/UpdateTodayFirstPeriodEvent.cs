using System;
using ENode.Eventing;

namespace Lottery.Core.Domain.LotteryFinalDatas
{
    public class UpdateTodayFirstPeriodEvent : DomainEvent<string>
    {
        private UpdateTodayFirstPeriodEvent()
        {
        }


        public UpdateTodayFirstPeriodEvent(int todayFirstPeriod,string lotteryId)
        {
            TodayFirstPeriod = todayFirstPeriod;
            LotteryId = lotteryId;
            UpdateTime = DateTime.Now;
        }


        public int TodayFirstPeriod { get; private set; }

        public string LotteryId { get; private set; }

        public DateTime UpdateTime { get; private set; }
    }
}