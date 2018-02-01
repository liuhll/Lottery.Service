using System;
using Lottery.Dtos.Lotteries;

namespace Lottery.RunApp.Events
{
    public class LotteryJobEventArgs : EventArgs
    {
        private readonly string _lotteryCode;

        private readonly string _lotteryId;
        private readonly LotteryFinalDataDto _lotteryFinalData;

        public LotteryJobEventArgs(string lotteryCode,
            string lotteryId,
            LotteryFinalDataDto lotteryFinalData)
        {
            _lotteryCode = lotteryCode;
            _lotteryId = lotteryId;
            _lotteryFinalData = lotteryFinalData;
        }

        public string LotteryCode => _lotteryCode;

        public string LotteryId => _lotteryId;

        public LotteryFinalDataDto LotteryFinalData => _lotteryFinalData;
    }
}