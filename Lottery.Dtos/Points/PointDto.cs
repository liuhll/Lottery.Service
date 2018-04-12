using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Points
{
    public class PointDto
    {
        public PointType PointType { get; set; }

        public int Point { get; set; }

        public string Notes { get; set; }

    }
}