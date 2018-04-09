using System;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Extensions;

namespace Lottery.Dtos.Points
{
    public class PointRecordOutput
    {
        public int Point { get; set; }

        public PointType PointType { get; set; }

        public string PointTypeDesc => PointType.GetChineseDescribe();

        public PointOperationType OperationType { get; set; }

        public string OperationTypeDesc => OperationType.GetChineseDescribe();

        public string Notes { get; set; }

        public DateTime SignedTime { get; set; }
    }
}
