using Lottery.Infrastructure.Enums;

namespace Lottery.Infrastructure.RunTime.Session
{
    public class NullLotterySession : LotterySessionBase
    {
        private NullLotterySession()
        {
        }

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static LotterySessionBase Instance { get; } = new ClaimsLotterySession();

        public override string UserId => null;
        public override string UserName => null;
        public override string Email => null;
        public override string Phone => null;
        public override string SystemTypeId => null;
        public override SystemType SystemType { get; }
        public override MemberRank MemberRank { get; }
    }
}