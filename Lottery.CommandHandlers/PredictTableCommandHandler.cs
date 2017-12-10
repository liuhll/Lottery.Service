using ENode.Commanding;
using Lottery.Commands.LotteryPredicts;
using Lottery.Core.Domain.LotteryPredictDatas;

namespace Lottery.CommandHandlers
{
    public class PredictTableCommandHandler : ICommandHandler<InitPredictTableCommand>
    {
        public void Handle(ICommandContext context, InitPredictTableCommand command)
        {
            context.Add(new PredictTable(command.AggregateRootId,command.PredictDbName,command.PredictTableNames));
        }
    }
}