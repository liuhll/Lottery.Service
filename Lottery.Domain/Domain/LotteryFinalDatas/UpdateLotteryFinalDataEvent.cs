using System;
using ENode.Eventing;
using Lottery.Core.Domain.LotteryDatas;
using Lottery.Infrastructure.Enums;

namespace Lottery.Core.Domain.LotteryFinalDatas
{
    public class UpdateLotteryFinalDataEvent : DomainEvent<string>
    {
        private UpdateLotteryFinalDataEvent()
        {
        }

        public UpdateLotteryFinalDataEvent(string lotteryId,int finalPeriod,string data,DateTime lotteryTime)
        {
            LotteryId = lotteryId;
            FinalPeriod = finalPeriod;
            Data = data;
            LotteryTime = lotteryTime;
            UpdateTime = DateTime.Now;
        }

        public string LotteryId { get; private set; }

        public int FinalPeriod { get; private set; }

        public string Data { get; private set; }

        public DateTime LotteryTime { get; private set; }

        public DateTime UpdateTime { get; private set; }
    }
}