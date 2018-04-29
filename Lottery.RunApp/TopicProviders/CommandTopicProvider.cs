using ENode.Commanding;
using ENode.EQueue;
using Lottery.Commands.LotteryDatas;
using Lottery.Commands.LotteryPredicts;
using Lottery.Infrastructure;

namespace Lottery.RunApp.TopicProviders
{
    public class CommandTopicProvider : AbstractTopicProvider<ICommand>
    {
        public CommandTopicProvider()
        {
            RegisterTopic(EQueueTopics.LotteryCommandTopic,
                typeof(AddLotteryDataCommand),
                typeof(UpdateNextDayFirstPeriodCommand),
                typeof(InitPredictTableCommand),
                typeof(CompleteDynamicTableCommand),
                typeof(PredictDataCommand)
               );
        }
    }
}