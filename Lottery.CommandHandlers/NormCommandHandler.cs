using ENode.Commanding;
using Lottery.Commands.Norms;
using Lottery.Core.Domain.UserNormDefaultConfig;

namespace Lottery.CommandHandlers
{
    public class NormCommandHandler : ICommandHandler<AddUserNormDefaultConfigCommand>,
        ICommandHandler<UpdateUserNormDefaultConfigCommand>
    {
        public void Handle(ICommandContext context, AddUserNormDefaultConfigCommand command)
        {
            context.Add(new UserNormDefaultConfig(command.AggregateRootId,command.UserId,command.LotteryId,command.PlanCycle,command.ForecastCount,command.UnitHistoryCount,command.MinRightSeries,command.MaxRightSeries,
                command.MinErrortSeries,command.MaxErrortSeries,command.LookupPeriodCount,command.ExpectMinScore,command.ExpectMaxScore));
        }

        public void Handle(ICommandContext context, UpdateUserNormDefaultConfigCommand command)
        {
            context.Get<UserNormDefaultConfig>(command.AggregateRootId).UpdateUserNormDefaultConfig(command.PlanCycle,
                command.ForecastCount, command.UnitHistoryCount, command.MinRightSeries, command.MaxRightSeries,
                command.MinErrortSeries, command.MaxErrortSeries, command.LookupPeriodCount, command.ExpectMinScore,
                command.ExpectMaxScore);
        }
    }
}