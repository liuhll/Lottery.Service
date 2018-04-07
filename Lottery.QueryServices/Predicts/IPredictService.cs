namespace Lottery.QueryServices.Predicts
{
    public interface IPredictService
    {
        void DeleteHistoryPredictDatas(string lotteryCode, string planNormTable, int lookupPeriodCount, int planCycle);
    }
}