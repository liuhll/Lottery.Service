using System.Collections.Generic;
using System.Linq;
using Dapper;
using ECommon.Components;
using Lottery.Core.Caching;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Extensions;
using Lottery.QueryServices.Lotteries;

namespace Lottery.QueryServices.Dapper.Lotteries
{
    [Component]
    public class LotteryDataQueryService : BaseQueryService,ILotteryDataQueryService
    {
        private readonly ICacheManager _cacheManager;

        public LotteryDataQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public ICollection<LotteryDataDto> GetAllDatas(string lotteryId, int count = 10000)
        {
            var redisKey = string.Format(RedisKeyConstants.LOTTERY_DATA_ALL_KEY, lotteryId.RemoveStrike());
            return _cacheManager.Get<ICollection<LotteryDataDto>>(redisKey, () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    var sql = $"SELECT TOP {count} * FROM dbo.L_LotteryData ORDER BY Period DESC";

                    return conn.Query<LotteryDataDto>(sql).ToList();
                }
            });
        }
    }
}