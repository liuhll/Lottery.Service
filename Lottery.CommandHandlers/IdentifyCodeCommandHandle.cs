using ENode.Commanding;
using Lottery.Commands.IdentifyCodes;
using Lottery.Core.Domain.IdentifyCode;

namespace Lottery.CommandHandlers
{
    public class IdentifyCodeCommandHandle : 
        ICommandHandler<AddIdentifyCodeCommand>,
        ICommandHandler<UpdateIdentifyCodeCommand>,
        ICommandHandler<InvalidIdentifyCodeCommand>
    {
        public void Handle(ICommandContext context, AddIdentifyCodeCommand command)
        {
            context.Add(new IdentifyCode(command.AggregateRootId,command.Receiver,command.Code,
                command.IdentifyCodeType,command.MessageType,command.ExpirationDate,command.CreateBy,
                command.CreateBy));
        }

        public void Handle(ICommandContext context, UpdateIdentifyCodeCommand command)
        {
            context.Get<IdentifyCode>(command.AggregateRootId).UpdateIdentifyCode(command.Code,command.ExpirationDate,command.UpdateBy);
        }

        public void Handle(ICommandContext context, InvalidIdentifyCodeCommand command)
        {
            context.Get<IdentifyCode>(command.AggregateRootId).InvalidIdentifyCode(command.UpdateBy);
        }
    }
}