using ENode.EQueue;
using ENode.Eventing;
using Lottery.Core.Domain.LotteryDatas;
using Lottery.Core.Domain.LotteryFinalDatas;
using Lottery.Core.Domain.LotteryInfos;
using Lottery.Core.Domain.LotteryPredictDatas;
using Lottery.Core.Domain.NormConfigs;
using Lottery.Core.Domain.UserInfos;
using Lottery.Core.Domain.UserNormDefaultConfig;
using Lottery.Core.Domain.UserTicket;
using Lottery.Infrastructure;

namespace Lottery.Tests.Providers
{
    public class EventTopicProvider : AbstractTopicProvider<IDomainEvent>
    {
        public EventTopicProvider()
        {
            RegisterTopic(EQueueTopics.LotteryEventTopic,
                typeof(LotteryDataAddedEvent),
                typeof(UpdateLotteryFinalDataEvent),
                typeof(InitPredictTableEvent),
                typeof(CompleteDynamicTableEvent));

            RegisterTopic(EQueueTopics.LotteryAccountEventTopic,
                typeof(AddUserTicketEvent),
                typeof(UpdateUserTicketEvent),
                typeof(InvalidAccessTokenEvent),
                typeof(AddUserInfoEvent),
                typeof(BindUserEmailEvent),
                typeof(BindUserPhoneEvent),
                typeof(UpdateLastLoginTimeEvent),
                typeof(UpdateLoginTimeEvent));

            RegisterTopic(EQueueTopics.NormEventTopic,
                typeof(AddUserNormDefaultConfigEvent),
                typeof(UpdateUserNormDefaultConfigEvent),
                typeof(AddNormConfigEvent));

        }
    }
}