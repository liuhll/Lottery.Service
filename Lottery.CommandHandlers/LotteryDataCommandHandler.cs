using ENode.Commanding;
using ENode.Infrastructure;
using Lottery.Commands.LotteryDatas;
using Lottery.Core.Domain.LotteryDatas;
using Lottery.Core.Domain.LotteryFinalDatas;

namespace Lottery.CommandHandlers
{
    public class LotteryDataCommandHandler :
        ICommandHandler<AddLotteryDataCommand>,
        ICommandHandler<UpdateNextDayFirstPeriodCommand>
    {
     //   private readonly ILockService _lockService;

        public LotteryDataCommandHandler( )
        {
           // _lockService = lockService;
        }

        public void Handle(ICommandContext context, AddLotteryDataCommand command)
        {
            context.Add(new LotteryData(command.AggregateRootId, command.LotteryId, command.Period, command.Data, command.LotteryTime));
        }

        public void Handle(ICommandContext context, UpdateNextDayFirstPeriodCommand command)
        {
            var aggreagate = context.Get<LotteryFinalData>(command.AggregateRootId);
            if (aggreagate == null)
            {
                context.Add(new LotteryFinalData(command.AggregateRootId, command.LotteryId, command.TodayFirstPeriod));
            }
            context.Get<LotteryFinalData>(command.AggregateRootId).UpdateFirstPeriod(command.TodayFirstPeriod, command.LotteryId);
        }
    }
}