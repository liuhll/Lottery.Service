using System.Collections.Generic;
using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Lotteries
{
    public class StatisticData
    {
        public double CurrentScore { get; set; }

        public int MaxSerieRight { get; set; }

        public int MaxSerieError { get; set; }

        public int CurrentSerie { get; set; }

        public IDictionary<int, int> MinorCycleStatistic { get; set; }
    }
}