using System;
using ENode.Eventing;

namespace Lottery.Core.Domain.LotteryDatas
{
    public class RunNewLotteryEvent : DomainEvent<string>
    {
        private RunNewLotteryEvent()
        {
        }

        public RunNewLotteryEvent(
           LotteryData lotteryData
        )
        {
            Period = lotteryData.Period;
            LotteryId = lotteryData.LotteryId;
            Data = lotteryData.Data;
            InsertTime = DateTime.Now;
            LotteryTime = lotteryData.LotteryTime;

        }

        /// <summary>
        /// 
        /// </summary>
        public int Period { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string LotteryId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? InsertTime { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LotteryTime { get; private set; }
    }
}