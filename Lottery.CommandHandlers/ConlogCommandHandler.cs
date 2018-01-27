using ENode.Commanding;
using Lottery.Commands.LogonLog;
using Lottery.Core.Domain.LogonLog;

namespace Lottery.CommandHandlers
{
    public class ConlogCommandHandler : ICommandHandler<AddConLogCommand>, 
        ICommandHandler<UpdateTokenCommand>,
        ICommandHandler<LogoutCommand>
    {
        private readonly ICommandService _commandService;

        public ConlogCommandHandler(ICommandService commandService)
        {
            _commandService = commandService;
        }

        public void Handle(ICommandContext context, AddConLogCommand command)
        {
            context.Add(new ConLog(command.AggregateRootId,command.ClientNo,command.SystemTypeId,command.Ip,command.UserId,command.InvalidDateTime,command.CreateBy));
        }

        public void Handle(ICommandContext context, UpdateTokenCommand command)
        {
            context.Get<ConLog>(command.AggregateRootId).UpdateToken(command.InvalidTime,command.UpdateBy);
        }

        public void Handle(ICommandContext context, LogoutCommand command)
        {
            context.Get<ConLog>(command.AggregateRootId).Logout(command.UpdateBy);
        }
    }
}