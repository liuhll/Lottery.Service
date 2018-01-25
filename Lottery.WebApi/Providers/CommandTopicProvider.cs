using ECommon.Components;
using ENode.Commanding;
using ENode.EQueue;
using Lottery.Commands.LogonLog;
using Lottery.Commands.LotteryDatas;
using Lottery.Commands.LotteryPredicts;
using Lottery.Commands.Norms;
using Lottery.Commands.UserInfos;
using Lottery.Infrastructure;

namespace Lottery.WebApi.Providers
{
    [Component]
    public class CommandTopicProvider : AbstractTopicProvider<ICommand>
    {
        public CommandTopicProvider()
        {
            RegisterTopic(EQueueTopics.LotteryCommandTopic,
                typeof(AddLotteryDataCommand),
                typeof(UpdateNextDayFirstPeriodCommand),
                typeof(InitPredictTableCommand),
                typeof(CompleteDynamicTableCommand)
            );

            RegisterTopic(EQueueTopics.LotteryAccountCommandTopic,
                typeof(AddLogonLogCommand),
                typeof(LogoutCommand),
                typeof(UpdateTokenCommand),
                typeof(AddUserInfoCommand),
                typeof(BindUserEmailCommand),
                typeof(BindUserPhoneCommand),
                typeof(UpdateUserLogintClientCountCommand)
                );

            RegisterTopic(EQueueTopics.NormCommandTopic,
                typeof(AddUserNormDefaultConfigCommand),
                typeof(UpdateUserNormDefaultConfigCommand),
                typeof(AddNormConfigCommand),
                typeof(UpdateNormConfigCommand),
                typeof(DeteteNormConfigCommand));
           
        }
    }
}