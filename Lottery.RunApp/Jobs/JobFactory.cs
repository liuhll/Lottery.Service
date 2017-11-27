using FluentScheduler;

namespace Lottery.RunApp.Jobs
{
    public class JobFactory : Registry
    {
        public JobFactory()
        {
            Schedule(()=> new DemoJob()).ToRunNow().AndEvery(5).Seconds();
        }
    }
}