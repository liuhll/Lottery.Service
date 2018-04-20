using ENode.Commanding;
using Lottery.Infrastructure.Enums;

namespace Lottery.Commands.Points
{
    public class AddPointRecordCommand : Command<string>
    {
        private AddPointRecordCommand()
        {
        }

        public AddPointRecordCommand(string id, int point, PointType pointType, PointOperationType operationType, string notes, string createBy) : base(id)
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