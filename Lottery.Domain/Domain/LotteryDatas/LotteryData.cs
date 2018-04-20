using ENode.Domain;
using Lottery.Core.Domain.LotteryFinalDatas;
using System;

namespace Lottery.Core.Domain.LotteryDatas
{
    public class LotteryData : AggregateRoot<string>
    {
        private LotteryDataInfo _lotteryDataInfo;

        public LotteryData(
          string id,
          string lotteryId,
          int peroid,
          string data,
          DateTime lotteryTime
          ) : base(id)
        {
            if (string.IsNullOrEmpty(lotteryId))
            {
                throw new Exception("彩种Id不允许为空");
            }
            if (string.IsNullOrEmpty(data))
            {
                throw new Exception("开奖数据不允许为空");
            }
            ApplyEvents(
                new UpdateLotteryFinalDataEvent(lotteryId, peroid, data, lotteryTime),
                new LotteryDataAddedEvent(new LotteryDataInfo(Id, peroid, lotteryId, data, lotteryTime)));
        }



        #region Handler Methods

        private void Handle(LotteryDataAddedEvent evnt)
        {
            _lotteryDataInfo = evnt.LotteryDataInfo;
        }

        private void Handle(UpdateLotteryFinalDataEvent evnt)
        {
        }

        #endregion Handler Methods
    }
}