using System;

namespace Lottery.Dtos.Norms
{
    public class NormPlanConfigDto
    {
        public string LotteryCode { get; set; }

        public string PredictCode { get; set; }

        public int MinForecastCount { get; set; }

        public int MaxForecastCount { get; set; }

        public int MinPlanCycle { get; set; }

        public int MaxPlanCycle { get; set; }
    }
}
