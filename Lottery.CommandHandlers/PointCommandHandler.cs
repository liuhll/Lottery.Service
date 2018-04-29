using ENode.Commanding;
using Lottery.Commands.Points;
using Lottery.Core.Domain.Points;

namespace Lottery.CommandHandlers
{
    public class PointCommandHandler : ICommandHandler<AddPointRecordCommand>
    {
        public void Handle(ICommandContext context, AddPointRecordCommand command)
        {
            context.Add(new PointRecord(command.AggregateRootId, command.Point, command.PointType, command.OperationType, command.Notes, command.CreateBy));
        }
    }
}