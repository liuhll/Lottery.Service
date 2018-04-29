using ENode.Commanding;
using Lottery.Commands.Messages;
using Lottery.Core.Domain.MessageRecords;

namespace Lottery.CommandHandlers
{
    public class MessageCommandHandler : ICommandHandler<AddMessageRecordCommand>
    {
        public void Handle(ICommandContext context, AddMessageRecordCommand command)
        {
            context.Add(new MessageRecord(command.AggregateRootId, command.Sender, command.Receiver,
                command.Title, command.Content, command.MessageType, command.SenderPlatform, command.CreateBy, command.CreateBy));
        }
    }
}