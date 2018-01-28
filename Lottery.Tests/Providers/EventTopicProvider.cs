using ENode.EQueue;
using ENode.Eventing;
using Lottery.Core.Domain.LogonLog;
using Lottery.Core.Domain.LotteryDatas;
using Lottery.Core.Domain.LotteryFinalDatas;
using Lottery.Core.Domain.LotteryInfos;
using Lottery.Core.Domain.LotteryPredictDatas;
using Lottery.Core.Domain.NormConfigs;
using Lottery.Core.Domain.UserInfos;
using Lottery.Core.Domain.UserNormDefaultConfig;
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
                typeof(CompleteDynamicTableEvent),
                typeof(AddLotteryPredictDataEvent));

            RegisterTopic(EQueueTopics.LotteryAccountEventTopic,
                typeof(AddConLogEvent),
                typeof(UpdateTokenEvent),
                typeof(LogoutEvent),
                typeof(AddUserInfoEvent),
                typeof(BindUserEmailEvent),
                typeof(BindUserPhoneEvent),
                typeof(UpdateLoginTimeEvent));

            RegisterTopic(EQueueTopics.NormEventTopic,
                typeof(AddUserNormDefaultConfigEvent),
                typeof(UpdateUserNormDefaultConfigEvent),
                typeof(AddNormConfigEvent));

        }
    }
}