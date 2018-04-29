using Lottery.Infrastructure.Enums;

namespace Lottery.Infrastructure.RunTime.Session
{
    public abstract class LotterySessionBase : ILotterySession
    {
        protected LotterySessionBase()
        {
        }

        public abstract string UserId { get; }

        //public abstract string TicketId { get; }
        public abstract string UserName { get; }

        public abstract string Email { get; }

        public abstract string Phone { get; }

        public abstract string SystemTypeId { get; }

        public abstract SystemType SystemType { get; }

        public abstract int ClientNo { get; }

        //public abstract MemberRank MemberRank { get; }
    }
}