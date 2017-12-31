namespace Lottery.WebApi.RunTime.Session
{
    public class NullLotterySession : LotterySessionBase
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static LotterySessionBase Instance { get; } = new ClaimsLotterySession();

        public override string UserId => null;
        public override string UserName => null;
        public override string Email => null;
        public override string Phone => null;

  
    }
}