using System;
using System.Collections.Generic;
using ENode.Commanding;
using Lottery.Commands.LogonLog;
using Lottery.Core.Domain.LogonLog;

namespace Lottery.CommandHandlers
{
    public class LogonCommandHandler : ICommandHandler<AddConLogCommand>, ICommandHandler<UpdateTokenCommand>
    {
        private readonly ICommandService _commandService;

        public LogonCommandHandler(ICommandService commandService)
        {
            _commandService = commandService;
        }

        public void Handle(ICommandContext context, AddConLogCommand command)
        {
            context.Add(new ConLog(command.AggregateRootId,command.ClientNo,command.SystemTypeId,command.Ip,command.UserId,command.InvalidDateTime,command.CreateBy));
        }

        public void Handle(ICommandContext context, UpdateTokenCommand command)
        {
            context.Get<ConLog>(command.AggregateRootId).UpdateToken(command.UserId,command.UpdateTokenTime,command.UpdateBy);
        }

      
    }
}