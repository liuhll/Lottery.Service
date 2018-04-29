using ENode.Commanding;

namespace Lottery.ProcessManagers
{
    public class ConLogProcessManager
    {
        private readonly ICommandService _commandService;

        public ConLogProcessManager(ICommandService commandService)
        {
            _commandService = commandService;
        }

        //public Task<AsyncTaskResult> HandleAsync(UpdateUserLogoutEvent evt)
        //{
        //    return _commandService.SendAsync(new UpdateUserLoginClientCountCommand(evt.UserId,false));
        //}
    }
}