using ENode.Commanding;
using Lottery.Commands.Norms;
using Lottery.Core.Domain.NormConfigs;
using Lottery.Core.Domain.UserNormDefaultConfig;

namespace Lottery.CommandHandlers
{
    public class NormCommandHandler : ICommandHandler<AddUserNormDefaultConfigCommand>,
        ICommandHandler<UpdateUserNormDefaultConfigCommand>,
        ICommandHandler<AddNormConfigCommand>,
        ICommandHandler<DeteteNormConfigCommand>,
        ICommandHandler<UpdateNormConfigCommand>
    {
        public void Handle(ICommandContext context, AddUserNormDefaultConfigCommand command)
        {
            context.Add(new UserNormDefaultConfig(command.AggregateRootId, command.UserId, command.LotteryId, command.PlanCycle, command.ForecastCount,
                command.UnitHistoryCount, command.HistoryCount, command.MinRightSeries, command.MaxRightSeries,
                command.MinErrortSeries, command.MaxErrortSeries, command.LookupPeriodCount, command.ExpectMinScore, command.ExpectMaxScore));
        }

        public void Handle(ICommandContext context, UpdateUserNormDefaultConfigCommand command)
        {
            context.Get<UserNormDefaultConfig>(command.AggregateRootId).UpdateUserNormDefaultConfig(command.PlanCycle,
                command.ForecastCount, command.UnitHistoryCount, command.HistoryCount, command.MinRightSeries, command.MaxRightSeries,
                command.MinErrortSeries, command.MaxErrortSeries, command.LookupPeriodCount, command.ExpectMinScore,
                command.ExpectMaxScore);
        }

        public void Handle(ICommandContext context, AddNormConfigCommand command)
        {
            context.Add(new NormConfig(command.AggregateRootId,
                command.UserId, command.LotteryId, command.PlanId, command.LastStartPeriod,
                command.PlanCycle, command.ForecastCount, command.UnitHistoryCount, command.HistoryCount,
                command.MinRightSeries, command.MaxRightSeries, command.MinErrorSeries,
                command.MaxErrorSeries, command.LookupPeriodCount, command.ExpectMinScore, command.ExpectMaxScore, command.Sort, command.CustomNumbers));
        }

        public void Handle(ICommandContext context, DeteteNormConfigCommand command)
        {
            context.Get<NormConfig>(command.AggregateRootId).DeleteNormConfig(command.AggregateRootId);
        }

        public void Handle(ICommandContext context, UpdateNormConfigCommand command)
        {
            context.Get<NormConfig>(command.AggregateRootId).UpdateNormConfig(command.LastStartPeriod,
                command.PlanCycle, command.ForecastCount, command.UnitHistoryCount, command.HistoryCount, command.MinRightSeries, command.MaxRightSeries, command.MinErrorSeries,
                command.MaxErrorSeries, command.LookupPeriodCount, command.ExpectMinScore, command.ExpectMaxScore, command.CustomNumbers);
        }
    }
}