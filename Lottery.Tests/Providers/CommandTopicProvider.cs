using ENode.Commanding;
using ENode.EQueue;
using Lottery.Commands.LotteryDatas;
using Lottery.Core.Domain.LotteryDatas;
using Lottery.Infrastructure;

namespace Lottery.Tests.Providers
{
    public class CommandTopicProvider : AbstractTopicProvider<ICommand>
    {
        public CommandTopicProvider()
        {
            RegisterTopic(EQueueTopics.RunLotteryCommandTopic, typeof(RunNewLotteryCommand));
        }
    }
}