using ENode.Commanding;
using ENode.EQueue;
using Lottery.Commands.LotteryDatas;
using Lottery.Infrastructure;

namespace Lottery.EventService
{
    public class CommandTopicProvider : AbstractTopicProvider<ICommand>
    {
        public CommandTopicProvider()
        {
            RegisterTopic(EQueueTopics.RunLotteryCommandTopic,typeof(RunNewLotteryCommand));
        }
    }
}