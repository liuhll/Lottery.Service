using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommon.Components;
using ENode.Commanding;
using ENode.Infrastructure;
using Lottery.Commands.LotteryDatas;
using Lottery.Core.Domain.LotteryDatas;

namespace Lottery.CommandHandlers
{
    public class LotteryCommandHandler : ICommandHandler<RunNewLotteryCommand>
    {
        private readonly ILockService _lockService;

        public LotteryCommandHandler(ILockService lockService)
        {
            _lockService = lockService;
        }

        public void Handle(ICommandContext context, RunNewLotteryCommand command)
        {
            _lockService.ExecuteInLock(typeof(LotteryData).Name, () =>
            {
                context.Add(new LotteryData(command.AggregateRootId, command.Period, command.LotteryId, command.Data, command.LotteryTime));
            });

        }
    }
}
