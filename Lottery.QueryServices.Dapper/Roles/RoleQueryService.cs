﻿using System.Collections.Generic;
using System.Linq;
using Dapper;
using ECommon.Components;
using ECommon.Dapper;
using Lottery.Core.Caching;
using Lottery.Dtos.RoleDto;
using Lottery.Infrastructure;
using Lottery.QueryServices.Roles;

namespace Lottery.QueryServices.Dapper.Roles
{
    [Component]
    public class RoleQueryService : BaseQueryService,IRoleQueryService
    {
        private readonly ICacheManager _cacheManager;

        public RoleQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public ICollection<RoleDto> GetUserRoles(string userId)
        {
            var redisKey = string.Format(RedisKeyConstants.USER_ROLE_KEY, userId);
            return _cacheManager.Get<ICollection<RoleDto>>(redisKey, () =>
            {
                var sql =
                    "SELECT * FROM dbo.F_Role AS A INNER JOIN dbo.F_UserRole AS B ON B.RoleId = A.Id WHERE A.IsDelete = 0 AND B.IsDelete =0 AND B.UserId=@UserId";
                using (var conn = GetLotteryConnection())
                {
                    return conn.Query<RoleDto>(sql, new {UserId = userId}).ToList();
                }
            });
        }
    }
}