using ENode.Eventing;
using Lottery.Infrastructure.Enums;

namespace Lottery.Core.Domain.Points
{
    public class AddPointRecordEvent : DomainEvent<string>
    {
        private AddPointRecordEvent()
        {
        }

        public AddPointRecordEvent(int point, PointType pointType, PointOperationType operationType, string notes, string createBy) 
        {
            Point = point;
            PointType = pointType;
            OperationType = operationType;
            Notes = notes;
            CreateBy = createBy;
        }

        public int Point { get; private set; }

        public PointType PointType { get; private set; }

        public PointOperationType OperationType { get; private set; }

        public string Notes { get; private set; }

        public string CreateBy { get; private set; }

    }
}