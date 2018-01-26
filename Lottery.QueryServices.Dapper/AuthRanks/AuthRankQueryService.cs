using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ECommon.Components;
using ECommon.Dapper;
using ECommon.Extensions;
using Lottery.Core.Caching;
using Lottery.Dtos.AuthRanks;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Enums;
using Lottery.QueryServices.AuthRanks;

namespace Lottery.QueryServices.Dapper.AuthRanks
{
    [Component]
    public class AuthRankQueryService : BaseQueryService,IAuthRankQueryService
    {
        private readonly ICacheManager _cacheManager;

        public AuthRankQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public AuthRankDto GetAuthRankByLotteryIdAndRank(string lotteryId, MemberRank memberRank)
        {
            return GetAuthRanksByLotteryId(lotteryId).Safe().FirstOrDefault(p => p.MemberRank == memberRank);
        }

        public ICollection<AuthRankDto> GetAuthRanksByLotteryId(string lotteryId)
        {
            var cacheKey = string.Format(RedisKeyConstants.LOTTERY_AUTHRANK_KEY,lotteryId);
            return _cacheManager.Get<IList<AuthRankDto>>(cacheKey, () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    conn.Open();
                    return conn.QueryList<AuthRankDto>(new {LotteryId = lotteryId}, TableNameConstants.AuthRankTable).ToList();
                }
            });
        }
    }
}