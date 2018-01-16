using System;
using System.Collections.Generic;
using System.Linq;
using ECommon.Components;
using ECommon.Dapper;
using ECommon.Extensions;
using Lottery.Core.Caching;
using Lottery.Dtos.Lotteries;
using Lottery.Dtos.Norms;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Exceptions;
using Lottery.QueryServices.Lotteries;

namespace Lottery.QueryServices.Dapper.Lotteries
{
    [Component]
    public class NormConfigQueryService : BaseQueryService, INormConfigQueryService
    {
        private readonly ICacheManager _cacheManager;

        public NormConfigQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }


        public ICollection<NormConfigDto> GetDefaultNormConfigs(string lotteryId)
        {
            var redisKey = string.Format(RedisKeyConstants.LOTTERY_NORMCONFIG_DEFAULT_KEY, lotteryId);
            return _cacheManager.Get<ICollection<NormConfigDto>>( redisKey,
                () =>
                {
                    using (var conn = GetLotteryConnection())
                    {
                        return conn.QueryList<NormConfigDto>(new{ IsDefualt = true},TableNameConstants.NormConfigTable).ToList();
                    }
                });
        }

        public ICollection<NormConfigDto> GetUserOrDefaultNormConfigs(string lotteryId,string userId = "")
        {
            if (string.IsNullOrEmpty(userId))
            {
                return GetDefaultNormConfigs(lotteryId);
            }
            var userNormConfigs = GetUserNormConfig(lotteryId,userId);
            if (userNormConfigs.Safe().Any())
            {
                return userNormConfigs;
            }
            return GetDefaultNormConfigs(lotteryId);
        }


        public ICollection<NormConfigDto> GetUserNormConfig(string lotteryId,string userId)
        {
            var redisKey = string.Format(RedisKeyConstants.LOTTERY_NORMCONFIG_LOTTERY_KEY,lotteryId, userId);
            return _cacheManager.Get<ICollection<NormConfigDto>>(redisKey,
                () =>
                {
                    using (var conn = GetLotteryConnection())
                    {
                        return conn.QueryList<NormConfigDto>(new { LotteryId = lotteryId, UserId = userId  }, TableNameConstants.NormConfigTable).ToList();
                    }
                });
        }

        public ICollection<NormConfigDto> GetPlanConfigDtos(string planId)
        {
            var redisKey = string.Format(RedisKeyConstants.LOTTERY_NORMCONFIG_KEY, planId);
            return _cacheManager.Get<ICollection<NormConfigDto>>(redisKey,
                () =>
                {
                    using (var conn = GetLotteryConnection())
                    {
                        return conn.QueryList<NormConfigDto>(new { PlanId = planId }, TableNameConstants.NormConfigTable).ToList();
                    }
                });
        }

        public UserPlanNormOutput GetUserNormConfigById(string userId,string normId)
        {
            using (var conn = GetLotteryConnection())
            {
                return conn.QueryList<UserPlanNormOutput>(new { Id = normId, UserId = userId }, TableNameConstants.NormConfigTable).First();
            }
        }

        public UserPlanNormOutput GetUserNormConfigByPlanId(string userId, string lotteryId, string planId)
        {
            using (var conn = GetLotteryConnection())
            {
                return conn.QueryList<UserPlanNormOutput>(new { LotteryId = lotteryId, PlanId= planId, UserId = userId }, TableNameConstants.NormConfigTable).First();
            }
        }
    }
}