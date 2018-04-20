using Lottery.Dtos.Lotteries;
using System.Collections.Generic;

namespace Lottery.QueryServices.Lotteries
{
    public interface ILotteryPredictDataQueryService
    {
        PredictDataDto GetLastPredictData(string predictId, string predictTable, string lotteryCode);

        PredictDataDto GetPredictDataByStartPeriod(int startPeriod, string normId, string planInfoPlanNormTable, string lotteryCode);

        ICollection<PredictDataDto> GetNormPredictDatas(string normId, string planInfoPlanNormTable, string lotteryCode);

        ICollection<PredictDataDto> GetNormHostoryPredictDatas(string normId, string planNormTable, int lookupPeriodCount, string lotteryCode);

        ICollection<PredictDataDto> GetNormPredictDatas(string normId, string planNormTable, int count, string lotteryCode);

        PredictDataDto GetNormCurrentPredictData(string userNormId, string planInfoPlanNormTable, string lotteryCode);
    }
}