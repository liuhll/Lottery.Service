using ECommon.Components;
using ENode.EQueue;
using ENode.Eventing;
using Lottery.Core.Domain.IdentifyCode;
using Lottery.Core.Domain.LogonLog;
using Lottery.Core.Domain.LotteryDatas;
using Lottery.Core.Domain.LotteryFinalDatas;
using Lottery.Core.Domain.LotteryInfos;
using Lottery.Core.Domain.LotteryPredictDatas;
using Lottery.Core.Domain.MessageRecords;
using Lottery.Core.Domain.NormConfigs;
using Lottery.Core.Domain.UserInfos;
using Lottery.Core.Domain.UserNormDefaultConfig;
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
                typeof(CompleteDynamicTableEvent),
                typeof(AddLotteryPredictDataEvent));

            RegisterTopic(EQueueTopics.LotteryAccountEventTopic,
                typeof(AddConLogEvent),
                typeof(UpdateTokenEvent),
                typeof(LogoutEvent),
                typeof(AddUserInfoEvent),
                typeof(BindUserEmailEvent),
                typeof(BindUserPhoneEvent),
                typeof(UpdateLoginTimeEvent),
                typeof(UpdateUserLoginClientCountEvent),
                typeof(UpdateUserLogoutEvent)
                );

            RegisterTopic(EQueueTopics.NormEventTopic,
                typeof(AddUserNormDefaultConfigEvent),
                typeof(UpdateUserNormDefaultConfigEvent),
                typeof(AddNormConfigEvent),
                typeof(DeleteNormConfigEvent),
                typeof(UpdateNormConfigEvent));

            RegisterTopic(EQueueTopics.MessageEventTopic,
                typeof(AddMessageRecordEvent),
                typeof(AddIdentifyCodeEvent),
                typeof(UpdateIdentifyCodeEvent),
                typeof(InvalidIdentifyCodeEvent));
        }
    }
}