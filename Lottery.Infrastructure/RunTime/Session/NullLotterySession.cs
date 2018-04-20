using Lottery.Infrastructure.Enums;

namespace Lottery.Infrastructure.RunTime.Session
{
    public class NullLotterySession : LotterySessionBase
    {
        private readonly ClaimsLotterySession _instance;

        private NullLotterySession()
        {
            _instance = new ClaimsLotterySession();
        }

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullLotterySession Instance = new NullLotterySession();

        public override string UserId => _instance.UserId;
        public override string UserName => _instance.UserName;
        public override string Email => _instance.Email;
        public override string Phone => _instance.Phone;
        public override int ClientNo => _instance.ClientNo;
        public override string SystemTypeId => _instance.SystemTypeId;
        public override SystemType SystemType => _instance.SystemType;
        public override MemberRank MemberRank => _instance.MemberRank;
    }
}