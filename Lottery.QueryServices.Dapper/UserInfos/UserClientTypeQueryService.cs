﻿using ECommon.Components;
using ECommon.Dapper;
using Lottery.Core.Caching;
using Lottery.Dtos.Account;
using Lottery.Infrastructure;
using Lottery.QueryServices.UserInfos;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.QueryServices.Dapper.UserInfos
{
    [Component]
    public class UserClientTypeQueryService : BaseQueryService, IUserClientTypeQueryService
    {
        private readonly ICacheManager _cacheManager;

        public UserClientTypeQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public ICollection<UserSystemTypeDto> GetUserSystemTypes(string userId)
        {
            using (var conn = GetLotteryConnection())
            {
                return conn.QueryList<UserSystemTypeDto>(new { UserId = userId }, TableNameConstants.UserClientTypeTable).ToList();
            }
        }
    }
}