using Dapper;
using ECommon.Components;
using Lottery.Core.Caching;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure;
using Lottery.QueryServices.Lotteries;

namespace Lottery.QueryServices.Dapper.Lotteries
{
    [Component]
    public class LotteryPredictDataQueryService : BaseQueryService, ILotteryPredictDataQueryService
    {
        private readonly ICacheManager _cacheManager;

        public LotteryPredictDataQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }


        public PredictDataDto GetLastPredictData(string predictId, string predictTable)
        {
            var sql =
                $@"SELECT TOP 1 [Id],[NormConfigId],[CurrentPredictPeriod],[StartPeriod],[EndPeriod],[MinorCycle],[PredictedData],[PredictedResult],[CurrentScore]
                                    FROM {predictTable} WHERE Id=@PredictId ORDER BY StartPeriod DESC";
            using (var conn = GetForecastLotteryConnection())
            {
                var redisKey = string.Format(RedisKeyConstants.LOTTERY_PREDICT_DATA_KEY, predictTable, predictId);
                conn.Open();
                return _cacheManager.Get<PredictDataDto>(redisKey, 
                    () => conn.QueryFirstOrDefault<PredictDataDto>(sql, new { PredictId = predictId }));
               
            }

        }
    }
}