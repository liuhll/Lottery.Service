using System;
using Dapper;
using ECommon.Components;
using Lottery.QueryServices.Predicts;

namespace Lottery.QueryServices.Dapper.Predicts
{
    [Component]
    public class PredictService :BaseQueryService, IPredictService
    {
        public void DeleteHistoryPredictDatas(string lotteryCode,string planNormTable, int lookupPeriodCount, int planCycle)
        {
            using (var conn = GetForecastLotteryConnection(lotteryCode))
            {
                var deleteCount = lookupPeriodCount * planCycle;
                var sql = $"DELETE FROM {planNormTable} WHERE Id IN (SELECT TOP {deleteCount} Id FROM {planNormTable} ORDER BY CurrentPredictPeriod DESC)";
                conn.Execute(sql);

            }
        }
    }
}