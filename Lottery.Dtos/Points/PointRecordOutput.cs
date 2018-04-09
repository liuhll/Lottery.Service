using System;
using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Points
{
    public class PointRecordOutput
    {
        public int Point { get; set; }

        public PointType PointType { get; set; }

        public PointOperationType OperationType { get; set; }

        public string Notes { get; set; }
    }
}
