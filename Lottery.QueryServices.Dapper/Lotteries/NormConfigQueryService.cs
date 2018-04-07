using System.Collections.Generic;
using System.Linq;
using Dapper;
using ECommon.Components;
using ECommon.Dapper;
using ECommon.Extensions;
using Lottery.Core.Caching;
using Lottery.Dtos.Lotteries;
using Lottery.Dtos.Norms;
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


        public ICollection<NormConfigDto> GetDefaultNormConfigs(string lotteryId)
        {
            var redisKey = string.Format(RedisKeyConstants.LOTTERY_NORMCONFIG_DEFAULT_KEY, lotteryId);
            return _cacheManager.Get<ICollection<NormConfigDto>>( redisKey,
                () =>
                {
                    using (var conn = GetLotteryConnection())
                    {
                        conn.Open();
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
            var userNormConfigs = GetUserNormConfigs(lotteryId,userId);
            if (userNormConfigs.Safe().Any())
            {
                return userNormConfigs;
            }
            return GetDefaultNormConfigs(lotteryId);
        }


        public ICollection<NormConfigDto> GetUserNormConfigs(string lotteryId,string userId)
        {
            var redisKey = string.Format(RedisKeyConstants.LOTTERY_NORMCONFIG_LOTTERY_KEY,lotteryId, userId);
            return _cacheManager.Get<ICollection<NormConfigDto>>(redisKey,
                () =>
                {
                    using (var conn = GetLotteryConnection())
                    {
                        conn.Open();
                        return conn.QueryList<NormConfigDto>(new { LotteryId = lotteryId, UserId = userId  }, TableNameConstants.NormConfigTable).ToList();
                    }
                });
        }

        public NormConfigDto GetUserNormConfig(string nromId)
        {
            using (var conn = GetLotteryConnection())
            {
                return conn.QueryList<NormConfigDto>(new { Id = nromId }, TableNameConstants.NormConfigTable).First();
            }
        }

        //public ICollection<NormConfigDto> GetPlanConfigDtos(string planId)
        //{
        //    var redisKey = string.Format(RedisKeyConstants.LOTTERY_NORMCONFIG_KEY, planId);
        //    return _cacheManager.Get<ICollection<NormConfigDto>>(redisKey,
        //        () =>
        //        {
        //            using (var conn = GetLotteryConnection())
        //            {
        //                return conn.QueryList<NormConfigDto>(new { PlanId = planId }, TableNameConstants.NormConfigTable).ToList();
        //            }
        //        });
        //}

        public UserPlanNormOutput GetUserNormConfigById(string userId,string normId)
        {
            using (var conn = GetLotteryConnection())
            {
                conn.Open();
                return conn.QueryList<UserPlanNormOutput>(new { Id = normId, UserId = userId }, TableNameConstants.NormConfigTable).First();
            }
        }

        public UserPlanNormOutput GetUserNormConfigByPlanId(string userId, string lotteryId, string planId)
        {
            using (var conn = GetLotteryConnection())
            {
                conn.Open();
                return conn.QueryList<UserPlanNormOutput>(new { LotteryId = lotteryId, PlanId= planId, UserId = userId }, TableNameConstants.NormConfigTable).FirstOrDefault();
            }
        }

        public PlanInfoDto GetNormPlanInfoByNormId(string normId,string lotteryId)
        {
            using (var conn = GetLotteryConnection())
            {
                conn.Open();
                var sql = @"SELECT TOP 1 B.*  FROM dbo.LA_NormConfig AS A
                           INNER JOIN dbo.L_PlanInfo AS B 
                           ON B.Id = a.PlanId
                           WHERE a.Id=@NormId AND A.LotteryId=@LotteryId
                          ";
                return conn.QueryFirst<PlanInfoDto>(sql, new {NormId = normId, LotteryId = lotteryId});
            }
        }
    }
}