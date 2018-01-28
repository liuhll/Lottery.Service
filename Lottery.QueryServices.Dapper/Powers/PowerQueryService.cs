using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using ECommon.Components;
using ECommon.Dapper;
using Lottery.Core.Caching;
using Lottery.Dtos.Power;
using Lottery.Infrastructure;
using Lottery.QueryServices.Powers;

namespace Lottery.QueryServices.Dapper.Powers
{
    [Component]
    public class PowerQueryService : BaseQueryService, IPowerQueryService
    {
        private readonly ICacheManager _cacheManager;

        public PowerQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public PowerDto GetPermissionByCode(string powerCode)
        {
            using (var conn = GetLotteryConnection())
            {
                return conn.QueryList<PowerDto>(new { PowerCode = powerCode, IsDelete = 0 },TableNameConstants.PowerTable).FirstOrDefault();
            }
        }

        public PowerDto GetPermissionByApi(string apiPath, string method)
        {
            using (var conn = GetLotteryConnection())
            {
                return conn.QueryList<PowerDto>(new { ApiPath = apiPath, HttpMethod = method, IsDelete = 0 }, TableNameConstants.PowerTable).FirstOrDefault();
            }
        }

        public ICollection<PowerDto> GetAppPowers()
        {
            using (var conn = GetLotteryConnection())
            {
                var sql = @"SELECT * FROM dbo.F_Power WHERE SystemType=0 OR PowerType=0 AND IsDelete= 0";
                return conn.Query<PowerDto>(sql).ToList();
            }
        }

        public ICollection<PowerDto> GetUserBoPowers(string userId)
        {
            using (var conn = GetLotteryConnection())
            {
                var sql = @"SELECT * FROM dbo.F_Power AS A 
                                INNER JOIN dbo.F_RolePower AS B ON B.PowerId = A.Id
                                INNER JOIN dbo.F_Role AS C ON C.Id = B.RoleId
                                INNER JOIN dbo.F_UserRole AS D ON D.RoleId = C.Id
                                INNER JOIN dbo.F_UserInfo AS E ON E.Id = D.UserId
                                WHERE E.Id = @UserId  AND A.IsDelete = 0 AND B.IsDelete = 0 
                                AND C.IsDelete = 0 AND D.IsDelete = 0 AND E.IsDelete = 0";
                return conn.Query<PowerDto>(sql,new { UserId = userId}).ToList();
            }
        }
    }
}