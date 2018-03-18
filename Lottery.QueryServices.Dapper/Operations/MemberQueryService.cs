using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ECommon.Components;
using ECommon.Dapper;
using ECommon.Extensions;
using Lottery.Core.Caching;
using Lottery.Dtos.Menbers;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Collections;
using Lottery.QueryServices.Operations;

namespace Lottery.QueryServices.Dapper.Operations
{
    [Component]
    public class MemberQueryService : BaseQueryService, IMemberQueryService
    {
        private readonly ICacheManager _cacheManager;

        public MemberQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public MemberInfoDto GetUserMenberInfo(string userId, string lotteryId)
        {
            return GetMenberInfos(lotteryId).Safe().FirstOrDefault(p=> p.UserId == userId);
        }

        public ICollection<MemberInfoDto> GetMenberInfos(string lotteryId)
        {
            var redisKey = string.Format(RedisKeyConstants.OPERATION_MEMBERINFO_KEY, lotteryId);
            return _cacheManager.Get<ICollection<MemberInfoDto>>(redisKey, () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    return conn.QueryList<MemberInfoDto>(new
                    {
                        LotteryId = lotteryId,
                        // Status = 0,
                    }, TableNameConstants.MemberTable).Safe().Where(p=>p.InvalidDate >= DateTime.Now).ToList();
                }

            });
        }
    }
}