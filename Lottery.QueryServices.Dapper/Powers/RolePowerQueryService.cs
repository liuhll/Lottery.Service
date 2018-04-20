using Dapper;
using ECommon.Components;
using Lottery.Core.Caching;
using Lottery.Dtos.Power;
using Lottery.Infrastructure;
using Lottery.QueryServices.Powers;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.QueryServices.Dapper.Powers
{
    [Component]
    public class RolePowerQueryService : BaseQueryService, IRolePowerQueryService
    {
        private readonly ICacheManager _cacheManager;

        public RolePowerQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public ICollection<PowerGrantInfo> GetPermissions(string roleId)
        {
            var redisKey = string.Format(RedisKeyConstants.ROLE_ROLEPOWER_KEY, roleId);
            return _cacheManager.Get<ICollection<PowerGrantInfo>>(redisKey, () =>
            {
                var sql = @"SELECT * FROM dbo.F_Power AS A
                            INNER JOIN dbo.F_RolePower AS B ON B.PowerId = A.Id
                            INNER JOIN dbo.F_Role AS C ON C.Id = B.RoleId
                            WHERE C.Id = @RoleId AND A.IsDelete = 0 AND B.IsDelete = 0 AND C.IsDelete = 0";
                using (var conn = GetLotteryConnection())
                {
                    var queryResult = conn.Query<dynamic>(sql, new { RoleId = roleId }).ToList();
                    return queryResult.Select(p => new PowerGrantInfo(p.PowerCode, true)).ToList();
                }
            });
        }
    }
}