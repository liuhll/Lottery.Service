using ENode.Commanding;
using Lottery.Dtos.Lotteries;
using System;

namespace Lottery.Commands.LotteryDatas
{
    public class AddLotteryDataCommand : Command
    {
        private AddLotteryDataCommand()
        {
        }

        public AddLotteryDataCommand(string id, LotteryDataDto lotteryDataDto) : base(id)
        {
            Period = lotteryDataDto.Period;
            LotteryId = lotteryDataDto.LotteryId;
            Data = lotteryDataDto.Data;
            LotteryTime = lotteryDataDto.LotteryTime;
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
        public DateTime LotteryTime { get; private set; }
    }
}