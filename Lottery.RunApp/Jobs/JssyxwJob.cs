namespace Lottery.RunApp.Jobs
{
    public class JssyxwJob : RunLotteryAbstractJob
    {
        protected override void PreInitialize()
        {
            _lotteryCode = "JSSYXW";
        }

        protected override void PostinItialize()
        {
        }
    }
}