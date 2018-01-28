using ENode.Commanding;
using Lottery.Commands.LotteryPredicts;
using Lottery.Core.Domain.LotteryPredictDatas;

namespace Lottery.CommandHandlers
{
    public class PredictDataCommandHandler : ICommandHandler<PredictDataCommand>
    {
        public void Handle(ICommandContext context, PredictDataCommand command)
        {
            context.Add(new LotteryPredictData(command.AggregateRootId,command.NormConfigId,command.CurrentPredictPeriod,
                command.StartPeriod,command.EndPeriod,command.MinorCycle,command.PredictedData,command.PredictedResult,
                command.CurrentScore,command.CreateBy,command.PredictTable,command.IsSwitchFormula));
        }
    }
}