using System.Linq;
using ECommon.Components;
using ECommon.Dapper;
using Lottery.Core.Caching;
using Lottery.Dtos.Norms;
using Lottery.Infrastructure;
using Lottery.QueryServices.Norms;

namespace Lottery.QueryServices.Dapper.Norms
{
    [Component]
    public class UserNormDefaultConfigService : BaseQueryService, IUserNormDefaultConfigService
    {
        private readonly ICacheManager _cacheManager;

        public UserNormDefaultConfigService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public UserNormDefaultConfigOutput GetUserNormOrDefaultConfig(string userId, string lotteryId)
        {
            var userNormDefaultRedisKey = string.Format(RedisKeyConstants.LOTTERY_USERNORM_DEFAULT_KEY, lotteryId);
            var userNormRedisKey = string.Format(RedisKeyConstants.LOTTERY_USERNORM_KEY, lotteryId, userId);
            var userNormConfig = _cacheManager.Get<UserNormDefaultConfigOutput>(userNormRedisKey, () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    return conn.QueryList<UserNormDefaultConfigOutput>(new { UserId = userId, LotteryId = lotteryId },
                        TableNameConstants.UserNormDefaultConfigTable).FirstOrDefault();
                }
            });
            if (userNormConfig == null)
            {
                userNormConfig = _cacheManager.Get<UserNormDefaultConfigOutput>(userNormDefaultRedisKey, () =>
                {
                    using (var conn = GetLotteryConnection())
                    {
                        return conn.QueryList<UserNormDefaultConfigOutput>(new { LotteryId = lotteryId },
                            TableNameConstants.UserNormDefaultConfigTable).FirstOrDefault();
                    }
                });
            }
            return userNormConfig;
        }

        public UserNormDefaultConfigDto GetUserNormConfig(string userId, string lotteryId)
        {

            using (var conn = GetLotteryConnection())
            {
                return conn.QueryList<UserNormDefaultConfigDto>(new { UserId = userId, LotteryId = lotteryId },
                    TableNameConstants.UserNormDefaultConfigTable).FirstOrDefault();
            }
            
        }
    }
}