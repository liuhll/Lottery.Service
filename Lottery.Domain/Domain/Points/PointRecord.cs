using ENode.Domain;
using Lottery.Infrastructure.Enums;
using System;

namespace Lottery.Core.Domain.Points
{
    public class PointRecord : AggregateRoot<string>
    {
        public PointRecord(string id, int point, PointType pointType, PointOperationType operationType, string notes, string createBy) : base(id)
        {
            Point = point;
            PointType = pointType;
            OperationType = operationType;
            Notes = notes;
            CreateBy = createBy;
            CreateTime = DateTime.Now;

            ApplyEvent(new AddPointRecordEvent(Point, PointType, OperationType, Notes, CreateBy));
        }

        public int Point { get; private set; }

        public PointType PointType { get; private set; }

        public PointOperationType OperationType { get; private set; }

        public string Notes { get; private set; }

        public string CreateBy { get; private set; }

        public DateTime CreateTime { get; private set; }

        #region handle

        private void Handle(AddPointRecordEvent evnt)
        {
            Point = evnt.Point;
            PointType = evnt.PointType;
            OperationType = evnt.OperationType;
            Notes = evnt.Notes;
            CreateBy = evnt.CreateBy;
            CreateTime = evnt.Timestamp;
        }

        #endregion handle
    }
}