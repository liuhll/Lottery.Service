using ECommon.Components;
using ENode.Commanding;
using ENode.EQueue;
using Lottery.Commands.LotteryDatas;
using Lottery.Infrastructure;

namespace Lottery.EventService
{
    [Component]
    public class CommandTopicProvider : AbstractTopicProvider<ICommand>
    {
        public CommandTopicProvider()
        {
            RegisterTopic(EQueueTopics.LotteryCommandPostTopic);
        }
    }
}