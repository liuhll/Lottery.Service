namespace Lottery.RunApp.Jobs
{
    public class CqsscJob : RunLotteryAbstractJob
    {
        protected override void PreInitialize()
        {
            _lotteryCode = "CQSSC";
        }

        protected override void PostinItialize()
        {
            
        }

        public override void Execute()
        {
            base.Execute();
        }
    }
}