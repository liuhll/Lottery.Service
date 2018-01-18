using Lottery.Infrastructure.Enums;

namespace Lottery.WebApi.RunTime.Session
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

        public abstract string ClientType { get; }

        public abstract MemberRank MemberRank { get; }
    }
}