using ENode.Commanding;

namespace Lottery.Commands.LotteryDatas
{
    public class UpdateNextDayFirstPeriodCommand : Command<string>
    {
        private UpdateNextDayFirstPeriodCommand()
        {
        }

        public UpdateNextDayFirstPeriodCommand(string id,string lotteryId,int todayFirstPeriod) : base(id)
        {
            LotteryId = lotteryId;
            TodayFirstPeriod = todayFirstPeriod;
        }

        public string LotteryId { get; private set; }

        public int TodayFirstPeriod { get;private set;}
    }
}