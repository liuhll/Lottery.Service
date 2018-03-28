using ENode.Commanding;
using Lottery.Commands.OpinionRecords;
using Lottery.Core.Domain.OpinionRecords;

namespace Lottery.CommandHandlers
{
    public class OpinionRecordHandler : ICommandHandler<AddOpinionRecordCommand>
    {
        public void Handle(ICommandContext context, AddOpinionRecordCommand command)
        {
            context.Add(new OpinionRecord(command.AggregateRootId,command.OpinionType,
                command.Content,command.Platform,command.ContactWay,command.CreateBy));
        }
    }
}