using System.Threading.Tasks;
using ECommon.IO;
using ENode.Commanding;
using ENode.Infrastructure;
using Lottery.Commands.UserInfos;
using Lottery.Core.Domain.UserInfos;

namespace Lottery.ProcessManagers
{
    public class ConLogProcessManager : IMessageHandler<UpdateLastLoginTimeEvent>
    {
        private readonly ICommandService _commandService;

        public ConLogProcessManager(ICommandService commandService)
        {
            _commandService = commandService;
        }


        public Task<AsyncTaskResult> HandleAsync(UpdateLastLoginTimeEvent evt)
        {
            return _commandService.SendAsync(new UpdateLastLoginTimeCommand(evt.UserId));
        }

        //public Task<AsyncTaskResult> HandleAsync(UpdateUserLogoutEvent evt)
        //{
        //    return _commandService.SendAsync(new UpdateUserLoginClientCountCommand(evt.UserId,false));
        //}
    }
}