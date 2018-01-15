using System;
using ENode.Commanding;
using ENode.Infrastructure;
using Lottery.Core.Domain.NormConfigs;

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