using Lottery.Dtos.Lotteries;

namespace Lottery.QueryServices.Lotteries
{
    public interface ILotteryPredictDataQueryService
    {
        PredictDataDto GetLastPredictData(string predictId, string predictTable);
    }
}