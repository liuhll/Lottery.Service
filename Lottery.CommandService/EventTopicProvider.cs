using ECommon.Components;
using ENode.EQueue;
using ENode.Eventing;
using Lottery.Commands.UserInfos;
using Lottery.Core.Domain.LotteryDatas;
using Lottery.Core.Domain.LotteryFinalDatas;
using Lottery.Core.Domain.LotteryInfos;
using Lottery.Core.Domain.LotteryPredictDatas;
using Lottery.Core.Domain.UserTicket;
using Lottery.Infrastructure;

namespace Lottery.CommandService
{
    [Component]
    public class EventTopicProvider : AbstractTopicProvider<IDomainEvent>
    {
        public EventTopicProvider()
        {
            RegisterTopic(EQueueTopics.LotteryEventTopic,
                typeof(LotteryDataAddedEvent),
                typeof(UpdateLotteryFinalDataEvent),
                typeof(UpdateTodayFirstPeriodEvent),
                typeof(InitPredictTableEvent),
                typeof(CompleteDynamicTableEvent));

            RegisterTopic(EQueueTopics.LotteryAccountEventTopic,
                typeof(AddUserTicketEvent),
                typeof(UpdateUserTicketEvent),
                typeof(InvalidAccessTokenEvent));
        }
    }
}