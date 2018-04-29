namespace Lottery.Engine.TimeRule
{
    public static class ITimeRuleManagerExtensions
    {
        public static bool PeriodIsLottery(this ITimeRuleManager timeRuleManager, int period)
        {
            return false;
        }
    }
}