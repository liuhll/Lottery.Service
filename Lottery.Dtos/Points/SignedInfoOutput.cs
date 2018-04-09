using System;

namespace Lottery.Dtos.Points
{
    public class SignedInfoOutput
    {
        public bool TodayIsSiged { get; set; }

        public int DurationDays { get; set; }

        public DateTime? LastSignedTime { get; set; }
    }
}