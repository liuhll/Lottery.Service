namespace Lottery.QueryServices.Lotteries
{
    public interface ILotteryFinalDataQueryService
    {
        LotteryFinalDataDto GetFinalData(string lotteryInfoId);
    }
}