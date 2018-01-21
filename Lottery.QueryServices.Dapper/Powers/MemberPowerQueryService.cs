using System.Collections.Generic;
using System.Linq;
using Dapper;
using ECommon.Components;
using Lottery.Core.Caching;
using Lottery.Dtos.Power;
using Lottery.Infrastructure;
using Lottery.QueryServices.Powers;

namespace Lottery.QueryServices.Dapper.Powers
{
    [Component]
    public class MemberPowerQueryService : BaseQueryService,IMemberPowerQueryService
    {
        private readonly ICacheManager _cacheManager;

        public MemberPowerQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public ICollection<PowerGrantInfo> GetMermberPermissions(string lotteryId, int memberRank)
        {
            var redisKey = string.Format(RedisKeyConstants.MEMBERRANK_MEMBERPOWER_KEY, lotteryId,memberRank);
            return _cacheManager.Get<ICollection<PowerGrantInfo>>(redisKey, () =>
            {
                var sql = @"SELECT * FROM dbo.F_Power AS A 
                            INNER JOIN dbo.F_RolePower AS B ON B.PowerId = A.Id
                            INNER JOIN dbo.MS_AuthRank AS C ON C.RoleId = B.RoleId
                            WHERE C.LotteryId=@LotteryId AND C.MemberRank=@MemberRank
                            AND C.Status=0 AND A.IsDelete = 0 AND B.IsDelete = 0";
                using (var conn = GetLotteryConnection())
                {
                    var queryResult = conn.Query<dynamic>(sql, new { LotteryId = lotteryId, MemberRank = memberRank }).ToList();
                    return queryResult.Select(p => new PowerGrantInfo(p.PowerCode, true)).ToList();
                }
            });
        }
    }
}