using ENode.EQueue;
using ENode.Eventing;
using Lottery.Core.Domain.LotteryDatas;
using Lottery.Infrastructure;

namespace Lottery.CommandService
{
    public class EventTopicProvider : AbstractTopicProvider<IDomainEvent>
    {
        public EventTopicProvider()
        {
            RegisterTopic(EQueueTopics.RunLotteryEventTopic,typeof(RunNewLotteryEvent));
        }
    }
}