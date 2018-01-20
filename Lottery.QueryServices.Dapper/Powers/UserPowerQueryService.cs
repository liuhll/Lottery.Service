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
    public class UserPowerQueryService : BaseQueryService,IUserPowerQueryService
    {
        private readonly ICacheManager _cacheManager;

        public UserPowerQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }


        public ICollection<PowerGrantInfo> GetPermissions(string userId)
        {
            var redisKey = string.Format(RedisKeyConstants.USERINFO_USERPOWER_KEY, userId);
            return _cacheManager.Get<ICollection<PowerGrantInfo>>(redisKey, () =>
            {
                var sql = @"SELECT * FROM dbo.F_Power AS A 
                                INNER JOIN dbo.F_RolePower AS B ON B.PowerId = A.Id
                                INNER JOIN dbo.F_Role AS C ON C.Id = B.RoleId
                                INNER JOIN dbo.F_UserRole AS D ON D.RoleId = C.Id
                                INNER JOIN dbo.F_UserInfo AS E ON E.Id = D.UserId
                                WHERE E.Id = @UserId  AND A.IsDelete = 0 AND B.IsDelete = 0 
                                AND C.IsDelete = 0 AND D.IsDelete = 0 AND E.IsDelete = 0";
                using (var conn = GetLotteryConnection())
                {

                    var queryResult = conn.Query<dynamic>(sql, new {UserId = userId}).ToList();
                    return queryResult.Select(p => new PowerGrantInfo(p.PowerCode,true)).ToList();
                }
            });

        }
    }
}