using System.Collections.Generic;
using System.Linq;
using Dapper;
using ECommon.Components;
using ECommon.Extensions;
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


        public PredictDataDto GetLastPredictData(string predictId, string predictTable,string lotteryCode)
        {
            var sql =
                $@"SELECT TOP 1 [Id],[NormConfigId],[CurrentPredictPeriod],[StartPeriod],[EndPeriod],[MinorCycle],[PredictedData],[PredictedResult],[CurrentScore]
                                    FROM {predictTable} WHERE NormConfigId=@PredictId ORDER BY StartPeriod DESC";
            using (var conn = GetForecastLotteryConnection(lotteryCode))
            {
                var redisKey = string.Format(RedisKeyConstants.LOTTERY_PREDICT_FINAL_DATA_KEY, predictTable, predictId);
                conn.Open();
                return _cacheManager.Get<PredictDataDto>(redisKey, 
                    () => conn.QueryFirstOrDefault<PredictDataDto>(sql, new { PredictId = predictId }));
               
            }

        }

        public PredictDataDto GetPredictDataByStartPeriod(int startPeriod, string normId, string predictTable, string lotteryCode)
        {
            var predictDatas = GetNormPredictDatas(normId, predictTable,lotteryCode);
            if (!predictDatas.Safe().Any())
            {
                return null;
            }
            return predictDatas.FirstOrDefault(p => p.StartPeriod == startPeriod);
        }

        public ICollection<PredictDataDto> GetNormPredictDatas(string normId, string predictTable, string lotteryCode)
        {
            var sql =
                $@"SELECT TOP 500 [Id],[NormConfigId],[CurrentPredictPeriod],[StartPeriod],[EndPeriod],[MinorCycle],[PredictedData],[PredictedResult],[CurrentScore]
                                    FROM {predictTable} WHERE NormConfigId=@NormConfigId ORDER BY StartPeriod DESC";
            using (var conn = GetForecastLotteryConnection(lotteryCode))
            {
                var redisKey = string.Format(RedisKeyConstants.LOTTERY_PREDICT_DATA_KEY, predictTable, normId);
                conn.Open();
                return _cacheManager.Get<ICollection<PredictDataDto>>(redisKey,
                    () => conn.Query<PredictDataDto>(sql, new { NormConfigId = normId }).ToList());

            }
        }

        public ICollection<PredictDataDto> GetNormHostoryPredictDatas(string normId, string planNormTable, int lookupPeriodCount,
            string lotteryCode)
        {
            return GetNormPredictDatas(normId, planNormTable, lotteryCode).Safe().Where(p=>p.PredictedResult != 2).Take(lookupPeriodCount).ToList();
        }

        public ICollection<PredictDataDto> GetNormPredictDatas(string normId, string planNormTable, int count, string lotteryCode)
        {
            return GetNormPredictDatas(normId, planNormTable, lotteryCode).Safe().Take(count).ToList();
        }

        public PredictDataDto GetNormCurrentPredictData(string normId, string planNormTable, string lotteryCode)
        {
            return GetNormPredictDatas(normId, planNormTable, lotteryCode).FirstOrDefault(p=>p.PredictedResult == 2);
        }
    }
}