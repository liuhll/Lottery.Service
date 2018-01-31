using System.Collections.Generic;

namespace Lottery.Dtos.Lotteries
{
    public class PlanTrackDetail
    {
        public string NormId { get; set; }

        public string PlanId { get; set; }

        public string PlanName { get; set; }

        public FinalLotteryDataOutput FinalLotteryData { get; set; }

        public StatisticData StatisticData { get; set; }

        public PredictDataDto CurrentPredictData { get; set; }

        public ICollection<PredictDataDto> HistoryPredictDatas { get; set; }

    }
}