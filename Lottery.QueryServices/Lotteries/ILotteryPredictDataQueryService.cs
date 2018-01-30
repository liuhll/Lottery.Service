using System.Collections.Generic;
using Lottery.Dtos.Lotteries;

namespace Lottery.QueryServices.Lotteries
{
    public interface ILotteryPredictDataQueryService
    {
        PredictDataDto GetLastPredictData(string predictId, string predictTable, string lotteryCode);
        PredictDataDto GetPredictDataByStartPeriod(int startPeriod, string normId, string planInfoPlanNormTable, string lotteryCode);

        ICollection<PredictDataDto> GetNormPredictDatas(string normId, string planInfoPlanNormTable, string lotteryCode);
        ICollection<PredictDataDto> GetNormHostoryPredictDatas(string normId, string planNormTable, int lookupPeriodCount, string lotteryCode);
    }
}