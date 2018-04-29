using ENode.Infrastructure;

namespace Lottery.CommandHandlers
{
    public class PlanCommandHandler
    {
        private readonly ILockService _lockService;

        public PlanCommandHandler(ILockService lockService)
        {
            _lockService = lockService;
        }
    }
}