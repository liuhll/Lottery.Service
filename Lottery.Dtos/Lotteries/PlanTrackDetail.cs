using System.Collections.Generic;

namespace Lottery.Dtos.Lotteries
{
    public class PlanTrackDetail
    {
        public string NormId { get; set; }

        public string PlanId { get; set; }

        public string PlanName { get; set; }

        public int Sort { get; set; }

        public FinalLotteryDataOutput FinalLotteryData { get; set; }

        public StatisticData StatisticData { get; set; }

        public PredictDataDetail CurrentPredictData { get; set; }

        public ICollection<PredictDataDetail> HistoryPredictDatas { get; set; }
    }
}