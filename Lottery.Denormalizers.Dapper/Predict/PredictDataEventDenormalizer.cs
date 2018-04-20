using Dapper;
using ECommon.Dapper;
using ECommon.IO;
using ENode.Infrastructure;
using Lottery.Core.Caching;
using Lottery.Core.Domain.LotteryPredictDatas;
using Lottery.Infrastructure;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Lottery.Denormalizers.Dapper
{
    public class PredictDataEventDenormalizer : AbstractDenormalizer, IMessageHandler<AddLotteryPredictDataEvent>
    {
        private readonly ICacheManager _cacheManager;

        public PredictDataEventDenormalizer(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<AsyncTaskResult> HandleAsync(AddLotteryPredictDataEvent evnt)
        {
            try
            {
                var sql = $"SELECT TOP 1 * FROM {evnt.PredictTable} WHERE NormConfigId=@NormConfigId AND StartPeriod=@StartPeriod";

                var cacheKey1 = string.Format(RedisKeyConstants.LOTTERY_PREDICT_DATA_KEY, evnt.PredictTable,
                    evnt.NormConfigId);
                var cacheKey2 = string.Format(RedisKeyConstants.LOTTERY_PREDICT_FINAL_DATA_KEY, evnt.PredictTable,
                    evnt.NormConfigId);
                _cacheManager.Remove(cacheKey1);
                _cacheManager.Remove(cacheKey2);

                using (var conn = GetForecastLotteryConnection(evnt.LotteryCode))
                {
                    conn.Open();
                    var predictData =
                        await conn.QueryFirstOrDefaultAsync(sql, new { evnt.NormConfigId, evnt.StartPeriod, evnt.EndPeriod });
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
                            }, new
                            {
                                evnt.NormConfigId,
                                evnt.StartPeriod,
                            },
                                evnt.PredictTable);
                        }
                        else
                        {
                            await conn.UpdateAsync(new
                            {
                                evnt.PredictedResult,
                                evnt.CurrentPredictPeriod,
                                evnt.MinorCycle,
                                evnt.CurrentScore,
                                UpdateBy = evnt.CreateBy,
                                UpdateTime = evnt.Timestamp,
                                evnt.EndPeriod
                            }, new
                            {
                                evnt.NormConfigId,
                                evnt.StartPeriod,
                            },
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