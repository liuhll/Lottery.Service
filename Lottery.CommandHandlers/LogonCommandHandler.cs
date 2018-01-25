using System.Collections.Generic;
using ENode.Commanding;
using Lottery.Commands.LogonLog;
using Lottery.Core.Domain.LogonLog;

namespace Lottery.CommandHandlers
{
    public class LogonCommandHandler : ICommandHandler<AddLogonLogCommand>, ICommandHandler<UpdateTokenCommand>, ICommandHandler<LogoutCommand>
    {
        private readonly ICommandService _commandService;

        public LogonCommandHandler(ICommandService commandService)
        {
            _commandService = commandService;
        }

        public void Handle(ICommandContext context, AddLogonLogCommand command)
        {
            context.Add(new LogonLog(command.AggregateRootId,command.UserId,command.CreateBy));
        }

        public void Handle(ICommandContext context, UpdateTokenCommand command)
        {
            context.Get<LogonLog>(command.AggregateRootId).UpdateToken(command.UserId,command.UpdateTokenTime,command.UpdateBy);
        }

        public void Handle(ICommandContext context, LogoutCommand command)
        {
            context.Get<LogonLog>(command.AggregateRootId).Logout();
        }
    }
}