using Lottery.Dtos.Lotteries;

namespace Lottery.QueryServices.Lotteries
{
    public interface ILotteryFinalDataQueryService
    {
        LotteryFinalDataDto GetFinalData(string lotteryInfoId);
    }
}