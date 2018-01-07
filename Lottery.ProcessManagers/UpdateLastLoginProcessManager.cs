using System.Threading.Tasks;
using ECommon.IO;
using ENode.Commanding;
using ENode.Infrastructure;
using Lottery.Commands.UserInfos;
using Lottery.Core.Domain.UserInfos;

namespace Lottery.ProcessManagers
{
    public class UpdateLastLoginProcessManager : IMessageHandler<UpdateLastLoginTimeEvent>
    {
        private readonly ICommandService _commandService;

        public UpdateLastLoginProcessManager(ICommandService commandService)
        {
            _commandService = commandService;
        }


        public Task<AsyncTaskResult> HandleAsync(UpdateLastLoginTimeEvent evt)
        {
            return _commandService.SendAsync(new UpdateLastLoginTimeCommand(evt.UserId));
        }
    }
}