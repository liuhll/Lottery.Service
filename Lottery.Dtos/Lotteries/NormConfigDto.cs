namespace Lottery.Dtos.Lotteries
{
    public class NormConfigDto
    {
        public string Id { get; set; }

        public string LotteryId { get; set; }

        public string UserId { get; set; }

        public string PlanId { get; set; }

        public int PlanCycle { get; set; }
        public int ForecastCount { get; set; }

        public int UnitHistoryCount { get; set; }

        public int HistoryCount { get; set; }

        public int LastStartPeriod { get; set; }

        public int MaxRightSeries { get; set; }

        public int MaxErrorSeries { get; set; }

        public double ExpectMaxScore { get; set; }

        public double ExpectMinScore { get; set; }

        public double ActualScore { get; set; }

        public int LookupPeriodCount { get; set; }

        public string CustomNumbers { get; set; }

    }
}