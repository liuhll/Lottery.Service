using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using ECommon.Dapper;
using ECommon.IO;
using ENode.Infrastructure;
using Lottery.Core.Domain.LotteryPredictDatas;

namespace Lottery.Denormalizers.Dapper
{
    public class PredictDataEventDenormalizer : AbstractDenormalizer, IMessageHandler<AddLotteryPredictDataEvent>
    {
        public async Task<AsyncTaskResult> HandleAsync(AddLotteryPredictDataEvent evnt)
        {
            try
            {
                var sql = $"SELECT TOP 1 * FROM {evnt.PredictTable} WHERE NormConfigId=@NormConfigId AND StartPeriod=@StartPeriod AND EndPeriod=@EndPeriod";
                using (var conn = GetForecastLotteryConnection())
                {
                    conn.Open();
                    var predictData =
                        await conn.QueryFirstOrDefaultAsync(sql, new {evnt.NormConfigId, evnt.StartPeriod, evnt.EndPeriod});
                    if (predictData == null)
                    {
                        await conn.InsertAsync(new
                        {
                            Id = evnt.AggregateRootId,
                            evnt.PredictedResult,
                            evnt.CurrentPredictPeriod,
                            evnt.MinorCycle,
                            evnt.CurrentScore,
                            evnt.NormConfigId,
                            evnt.StartPeriod,
                            evnt.EndPeriod,
                            evnt.PredictedData,
                            evnt.CreateBy,
                            CreateTime = evnt.Timestamp
                        }, evnt.PredictTable);
                    }
                    else
                    {
                        if (predictData.PredictedResult == 2)
                        {
                            await conn.UpdateAsync(new
                                {
                                    evnt.PredictedResult,
                                    evnt.CurrentPredictPeriod,
                                    evnt.MinorCycle,
                                    evnt.CurrentScore,
                                    UpdateBy = evnt.CreateBy,
                                    UpdateTime = evnt.Timestamp
                                }, new {
                                    evnt.NormConfigId,
                                    evnt.StartPeriod,
                                    evnt.EndPeriod}, 
                                evnt.PredictTable);
                        }
                    }
                }
                return AsyncTaskResult.Success;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)  //主键冲突，忽略即可；出现这种情况，是因为同一个消息的重复处理
                {
                    return AsyncTaskResult.Success;
                }
                throw new IOException("Insert record failed.", ex);
            }
        }
    }
}