﻿using System;
using System.Collections.Generic;
using System.Linq;
using ECommon.Components;
using ECommon.Dapper;
using ECommon.Extensions;
using Lottery.Core.Caching;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure;
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


        public ICollection<NormConfigDto> GetDefaultNormConfigs()
        {
            return _cacheManager.Get<ICollection<NormConfigDto>>(RedisKeyConstants.LOTTERY_NORMCONFIG_DEFAULT_KEY,
                () =>
                {
                    using (var conn = GetLotteryConnection())
                    {
                        return conn.QueryList<NormConfigDto>(new{ IsDefualt = true},TableNameConstants.NormConfigTable).ToList();
                    }
                });
        }

        public ICollection<NormConfigDto> GetUserOrDefaultNormConfigs(string userId = "")
        {
            if (string.IsNullOrEmpty(userId))
            {
                return GetDefaultNormConfigs();
            }
            var userNormConfigs = GetUserNormConfig(userId);
            if (userNormConfigs.Safe().Any())
            {
                return userNormConfigs;
            }
            return GetDefaultNormConfigs();
        }


        public ICollection<NormConfigDto> GetUserNormConfig(string userId)
        {
            var redisKey = string.Format(RedisKeyConstants.LOTTERY_NORMCONFIG_KEY, userId);
            return _cacheManager.Get<ICollection<NormConfigDto>>(redisKey,
                () =>
                {
                    using (var conn = GetLotteryConnection())
                    {
                        return conn.QueryList<NormConfigDto>(new { UserId = userId }, TableNameConstants.NormConfigTable).ToList();
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
    }
}