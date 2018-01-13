using System.Collections.Generic;
using System.Linq;
using Dapper;
using ECommon.Components;
using ECommon.Dapper;
using Lottery.Core.Caching;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure;
using Lottery.QueryServices.Lotteries;

namespace Lottery.QueryServices.Dapper.Lotteries
{
    [Component]
    public class LotteryQueryService : BaseQueryService, ILotteryQueryService
    {

        protected readonly ICacheManager _cacheManager;

        public LotteryQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public LotteryInfoDto GetLotteryInfoByCode(string lotteryCode)
        {
            var redisKey = string.Format(RedisKeyConstants.LOTTERY_INFO_KEY, lotteryCode);

            return _cacheManager.Get<LotteryInfoDto>(redisKey, () =>
            {
                if (GetAllLotteryInfo().Any())
                {
                    return GetAllLotteryInfo().First(p => p.LotteryCode == lotteryCode);
                }
                return null;

            });
        }

        public ICollection<LotteryInfoDto> GetAllLotteryInfo()
        {
            return _cacheManager.Get<IList<LotteryInfoDto>>(RedisKeyConstants.LOTTERY_INFO_ALL_KEY, () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    return conn.QueryList<LotteryInfoDto>(null,TableNameConstants.LotteryInfoTable).ToList();
                }
            });
        }

        public LotteryInfoDto GetLotteryInfoById(string lotteryId)
        {
            var redisKey = string.Format(RedisKeyConstants.LOTTERY_INFO_KEY, lotteryId);

            return _cacheManager.Get<LotteryInfoDto>(redisKey, () =>
            {
                if (GetAllLotteryInfo().Any())
                {
                    return GetAllLotteryInfo().First(p => p.Id == lotteryId);
                }
                return null;

            });
        }
    }
}