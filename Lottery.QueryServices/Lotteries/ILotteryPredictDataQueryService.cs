using System.Collections.Generic;
using Lottery.Dtos.Lotteries;

namespace Lottery.QueryServices.Lotteries
{
    public interface ILotteryPredictDataQueryService
    {
        PredictDataDto GetLastPredictData(string predictId, string predictTable);
        PredictDataDto GetPredictDataByStartPeriod(int startPeriod, string normId, string planInfoPlanNormTable);

        ICollection<PredictDataDto> GetNormPredictDatas(string normId, string planInfoPlanNormTable);
    }
}