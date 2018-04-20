using ENode.Domain;
using System;

namespace Lottery.Core.Domain.LotteryFinalDatas
{
    public class LotteryFinalData : AggregateRoot<string>
    {
        private LotteryFinalData()
        {
        }

        public LotteryFinalData(
         string id,
         string lotteryId,
         int todayFirstPeriod
         ) : base(id)
        {
            LotteryId = lotteryId;
            TodayFirstPeriod = todayFirstPeriod;
        }

        /// <summary>
        /// 彩种Id
        /// </summary>
        public string LotteryId { get; private set; }

        /// <summary>
        /// 期数
        /// </summary>
        public int FinalPeriod { get; private set; }

        /// <summary>
        /// 今日开奖的第一期
        /// </summary>
        public int TodayFirstPeriod { get; private set; }

        #region public methods

        public void UpdateFirstPeriod(int todayFirstPeriod, string lotteryId)
        {
            if (string.IsNullOrEmpty(lotteryId))
            {
                throw new Exception("LotteryId 不允许为空");
            }
            ApplyEvent(new UpdateTodayFirstPeriodEvent(todayFirstPeriod, lotteryId));
        }

        #endregion public methods

        #region handle methods

        private void Handle(UpdateTodayFirstPeriodEvent evnt)
        {
            TodayFirstPeriod = evnt.TodayFirstPeriod;
            LotteryId = evnt.LotteryId;
        }

        #endregion handle methods
    }
}