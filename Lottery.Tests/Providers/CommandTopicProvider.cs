using ENode.Commanding;
using ENode.EQueue;
using Lottery.Commands.LogonLog;
using Lottery.Commands.LotteryDatas;
using Lottery.Commands.LotteryPredicts;
using Lottery.Commands.Messages;
using Lottery.Commands.Norms;
using Lottery.Commands.UserInfos;
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
                typeof(CompleteDynamicTableCommand),
                typeof(PredictDataCommand));

            RegisterTopic(EQueueTopics.LotteryAccountCommandTopic,
                typeof(AddConLogCommand),
                typeof(AddUserInfoCommand),
                typeof(BindUserEmailCommand),
                typeof(BindUserPhoneCommand),
                typeof(UpdateLastLoginTimeCommand));

            RegisterTopic(EQueueTopics.NormCommandTopic,
                typeof(AddUserNormDefaultConfigCommand),
                typeof(UpdateUserNormDefaultConfigCommand),
                typeof(AddNormConfigCommand),
                typeof(UpdateNormConfigCommand));

            RegisterTopic(EQueueTopics.MessageCommandTopic,
                typeof(AddMessageRecordCommand));
        }
    }
}