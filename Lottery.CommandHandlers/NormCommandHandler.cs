using ENode.Commanding;
using Lottery.Commands.Norms;
using Lottery.Core.Domain.UserNormDefaultConfig;

namespace Lottery.CommandHandlers
{
    public class NormCommandHandler : ICommandHandler<AddUserNormDefaultConfigCommand>
    {
        public void Handle(ICommandContext context, AddUserNormDefaultConfigCommand command)
        {
            context.Add(new UserNormDefaultConfig(command.Id,command.UserId,command.LotteryId,command.PlanCycle,command.ForecastCount,command.UnitHistoryCount,command.MinRightSeries,command.MaxRightSeries,
                command.MinErrortSeries,command.MaxErrortSeries,command.LookupPeriodCount,command.ExpectMinScore,command.ExpectMaxScore));
        }
    }
}