using ENode.Eventing;

namespace Lottery.Core.Domain.NormConfigs
{
    public class DeleteNormConfigEvent : DomainEvent<string>
    {
        private DeleteNormConfigEvent()
        {
        }

        public DeleteNormConfigEvent(string userId,string lotteryId)
        {
            UserId = userId;
            LotteryId = lotteryId;
        }
        public string UserId { get; private set; }

        public string LotteryId { get; private set; }

    }
}