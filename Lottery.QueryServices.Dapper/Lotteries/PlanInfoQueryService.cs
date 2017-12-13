using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using ECommon.Components;
using Lottery.Core.Caching;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Extensions;
using Lottery.QueryServices.Lotteries;

namespace Lottery.QueryServices.Dapper.Lotteries
{
    [Component]
    public class PlanInfoQueryService : BaseQueryService,IPlanInfoQueryService
    {
        private readonly ICacheManager _cacheManager;

        public PlanInfoQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public PlanInfoDto GetPlanInfoByCode(string planCode)
        {
            var redisKey = string.Format(RedisKeyConstants.LOTTERY_PLAN_KEY, planCode);
            return _cacheManager.Get<PlanInfoDto>(redisKey, () =>
            {
                return GetAll().FirstOrDefault(p => p.PlanCode == planCode);
            });
        }

        public PlanInfoDto GetPlanInfoById(string planId)
        {
            var redisKey = string.Format(RedisKeyConstants.LOTTERY_PLAN_KEY, planId);
            return _cacheManager.Get<PlanInfoDto>(redisKey, () =>
            {
                return GetAll().FirstOrDefault(p => p.Id == planId);
            });
        }

        //public PlanInfoDto GetPlanInfoById(string planId)
        //{
        //    var redisKey = string.Format(RedisKeyConstants.LOTTERY_PLAN_KEY, planId);
        //    return _cacheManager.Get<PlanInfoDto>(redisKey, () =>
        //    {
        //        return GetAll().FirstOrDefault(p => p.PlanCode == planId);
        //    });
        //}

        public ICollection<PlanInfoDto> GetAll()
        {
            return _cacheManager.Get<ICollection<PlanInfoDto>>(RedisKeyConstants.LOTTERY_PLAN_ALL_KEY, () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    conn.Open();

                    var querySql = @"SELECT A.*,B.*,C.*,D.* FROM dbo.L_PlanInfo AS A 
                                    INNER JOIN dbo.L_LotteryInfo AS B ON A.LotteryId=B.Id
                                    INNER JOIN dbo.L_PlanKeyNumber AS C ON C.PlanId=A.Id
                                    INNER JOIN dbo.L_PositionInfo AS D ON D.Id=C.PositionId";
                    var lookUp = new Dictionary<string, PlanInfoDto>();

                    conn.Query<PlanInfoDto,LotteryInfoDto,dynamic,PositionInfoDto,PlanInfoDto>(querySql, (planInfo,lotteryInfo,obj,positionInfo) =>
                    {
                        PlanInfoDto p;
                        if (!lookUp.TryGetValue(planInfo.PlanCode,out p))
                        {
                            planInfo.LotteryInfo = lotteryInfo;
                            planInfo.PositionInfos = new List<PositionInfoDto>();
                            lookUp.Add(planInfo.PlanCode, p = planInfo);

                        }
                        positionInfo.NumberType = (NumberType)Convert.ToInt32(obj.NumberType);
                        p.PositionInfos.Add(positionInfo);

                        return p;
                    });
                    return lookUp.Values;
                }
            });
        }

        public ICollection<PlanInfoDto> GetPlanInfoByLotteryId(string lotteryId)
        {
            var redisKey = string.Format(RedisKeyConstants.LOTTERY_PLAN_KEY, lotteryId);
            return _cacheManager.Get<ICollection<PlanInfoDto>>(redisKey, () =>
            {
                return GetAll().Where(p => p.LotteryInfo.Id == lotteryId).ToList();
            });
        }
    }
}