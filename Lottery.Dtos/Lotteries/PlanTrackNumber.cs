using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Lotteries
{
    public class PlanTrackNumber
    {
        public string NormId { get; set; }
        public string PlanId { get; set; }

        public string PlanName { get; set; }

        public int StartPeriod { get; set; }

        public int EndPeriod { get; set; }

        public int CurrentPredictPeriod { get; set; }

        public int MinorCycle { get; set; }

        public string PredictData { get; set; }

        public PredictType PredictType { get; set; }

        public int[] HistoryPredictResults { get; set; }

        public double CurrentScore { get; set; }

        public int Sort { get; set; }
    }
}