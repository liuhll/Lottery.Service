using System;

namespace Lottery.Core.Domain.LotteryDatas
{
    [Serializable]
    public class LotteryDataInfo
    {
        public LotteryDataInfo(string lotteryDataId, int period, string lotteryId, string data, DateTime lotteryTime)
        {
            LotteryDataId = lotteryDataId;
            Period = period;
            LotteryId = lotteryId;
            Data = data;
            LotteryTime = lotteryTime;
            InsertTime = DateTime.Now;
        }

        public string LotteryDataId { get; private set; }

        public int Period { get; private set; }

        public string LotteryId { get; private set; }

        public string Data { get; private set; }

        public DateTime LotteryTime { get; private set; }

        public DateTime InsertTime { get; private set; }
    }
}