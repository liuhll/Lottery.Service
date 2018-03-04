using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Lotteries
{
    public class PredictDataDto
    {
        public string NormConfigId { get; set; }

        public int CurrentPredictPeriod { get; set; }

        public PredictType PredictType { get; set; }

        public int StartPeriod { get; set; }

        public int EndPeriod { get; set; }

        public int MinorCycle { get; set; }

        public string PredictedData { get; set; }

        public int PredictedResult { get; set; }

        public double CurrentScore { get; set; }


    }
}