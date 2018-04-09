using System;

namespace Lottery.Dtos.Points
{
    public class SignedDto
    {
        public string UserId { get; set; }

        public DateTime CurrentPeriodStartDate { get; set; }

        public DateTime CurrentPeriodEndDate { get; set; }

        public int DurationDays { get; set; }

        public int DistanceLastPeriodDays { get; set; }
    }
}