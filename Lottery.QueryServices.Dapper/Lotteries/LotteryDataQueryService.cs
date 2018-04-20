using Dapper;
using ECommon.Components;
using Lottery.Core.Caching;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure;
using Lottery.QueryServices.Lotteries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.QueryServices.Dapper.Lotteries
{
    [Component]
    public class LotteryDataQueryService : BaseQueryService, ILotteryDataQueryService
    {
        private readonly ICacheManager _cacheManager;

        public LotteryDataQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public ICollection<LotteryDataDto> GetAllDatas(string lotteryId, int count = 10000)
        {
            var redisKey = string.Format(RedisKeyConstants.LOTTERY_DATA_ALL_KEY, lotteryId);
            return _cacheManager.Get<ICollection<LotteryDataDto>>(redisKey, () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    var sql = $"SELECT TOP {count} * FROM dbo.L_LotteryData WHERE lotteryId=@lotteryId ORDER BY Period DESC";

                    return conn.Query<LotteryDataDto>(sql, new { @lotteryId = lotteryId }).ToList();
                }
            });
        }

        public ICollection<LotteryDataDto> GetPredictPeriodDatas(string lotteryId, int predictPeriod, int userNormHistoryCount)
        {
            var result = GetAllDatas(lotteryId).Where(p => p.Period < predictPeriod).Take(userNormHistoryCount).ToList();

            return result;
        }

        public LotteryDataDto GetPredictPeriodData(string lotteryId, int period)
        {
            return GetAllDatas(lotteryId).FirstOrDefault(p => p.Period == period);
        }

        public ICollection<LotteryDataDto> GetLotteryDatas(string lotteryId, DateTime lotteryTime)
        {
            var redisKey = string.Format(RedisKeyConstants.LOTTERY_DATA_DAY_KEY, lotteryId, lotteryTime.ToString("yyyyMMdd"));
            return _cacheManager.Get<ICollection<LotteryDataDto>>(redisKey, () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    var sql = $"SELECT * FROM dbo.L_LotteryData WHERE lotteryId=@lotteryId AND DATEDIFF(dd,LotteryTime,@lotteryTime)=0  ORDER BY Period DESC";

                    return conn.Query<LotteryDataDto>(sql, new { @lotteryId = lotteryId, @lotteryTime = lotteryTime }).ToList();
                }
            });
        }
    }
}