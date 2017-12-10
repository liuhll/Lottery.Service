using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Lotteries
{
    public class PositionInfoDto
    {
        public string Name { get; set; }

        public PositionType PositionType { get; set; }

        public int Position { get; set; }

        public int MaxValue { get; set; }

        public int MinValue { get; set; }

        public NumberType NumberType { get; set; }
    }
}