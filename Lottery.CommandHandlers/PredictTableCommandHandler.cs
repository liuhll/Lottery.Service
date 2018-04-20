using ENode.Commanding;
using Lottery.Commands.LotteryDatas;
using Lottery.Commands.LotteryPredicts;
using Lottery.Core.Domain.LotteryInfos;
using Lottery.Core.Domain.LotteryPredictDatas;

namespace Lottery.CommandHandlers
{
    public class PredictTableCommandHandler : ICommandHandler<InitPredictTableCommand>,
        ICommandHandler<CompleteDynamicTableCommand>
    {
        public void Handle(ICommandContext context, InitPredictTableCommand command)
        {
            context.Add(new PredictTable(command.AggregateRootId, command.PredictDbName, command.LotteryCode, command.PredictTableNames));
        }

        public void Handle(ICommandContext context, CompleteDynamicTableCommand command)
        {
            var aggreagate = context.Get<LotteryInfo>(command.AggregateRootId);
            if (aggreagate == null)
            {
                context.Add(new LotteryInfo(command.AggregateRootId));
            }
            context.Get<LotteryInfo>(command.AggregateRootId).CompleteDynamicTable(command.IsComplteDynamicTable);
        }
    }
}