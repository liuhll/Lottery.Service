using ENode.Commanding;
using ENode.EQueue;
using Lottery.Commands.LotteryDatas;
using Lottery.Commands.LotteryPredicts;
using Lottery.Infrastructure;

namespace Lottery.Tests.Providers
{
    public class CommandTopicProvider : AbstractTopicProvider<ICommand>
    {
        public CommandTopicProvider()
        {
            RegisterTopic(EQueueTopics.LotteryCommandTopic,
                typeof(AddLotteryDataCommand),
                typeof(InitPredictTableCommand),
                typeof(CompleteDynamicTableCommand));
        }
    }
}