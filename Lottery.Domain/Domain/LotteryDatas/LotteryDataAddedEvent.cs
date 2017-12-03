using ENode.Eventing;

namespace Lottery.Core.Domain.LotteryDatas
{
    public class LotteryDataAddedEvent : DomainEvent<string>
    {
        private LotteryDataAddedEvent()
        {
        }

        public LotteryDataAddedEvent(LotteryDataInfo lotteryData)
        {
            LotteryDataInfo = lotteryData;
        }

        public LotteryDataInfo LotteryDataInfo { get; private set; }
    }
}