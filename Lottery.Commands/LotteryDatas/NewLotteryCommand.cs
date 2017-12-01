using System;
using ENode.Commanding;

namespace Lottery.Commands.LotteryDatas
{
    public class NewLotteryCommand : Command
    {
        private NewLotteryCommand()
        {
        }

        public NewLotteryCommand(
            string id,
            int period,
            string lotteryId,
            string data,
            DateTime? lotteryTime
        ) : base(id)
        {
            Period = period;
            LotteryId = lotteryId;
            Data = data;
            InsertTime = DateTime.Now;
            LotteryTime = lotteryTime;

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
