using System;
using System.Collections.Generic;
using Dapper;
using ECommon.Components;
using Lottery.Core.Caching;
using Lottery.Dtos.Plans;
using Lottery.Infrastructure;
using Lottery.QueryServices.Lotteries;

namespace Lottery.QueryServices.Dapper.Lotteries
{

    [Component]
    public class NormGroupQueryService : BaseQueryService, INormGroupQueryService
    {
        private readonly ICacheManager _cacheManager;

        public NormGroupQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public ICollection<NormGroupOutput> GetNormGroups(string lotteryId)
        {
            using (var conn = GetLotteryConnection())
            {
                conn.Open();
                var redisKey = RedisKeyConstants.LOTTERY_NORMGROUP_ALL_KEY;
                var sql = @"SELECT * FROM dbo.L_NormGroup AS A 
                          INNER JOIN dbo.L_PlanInfo AS B ON  B.NormGroupId = A.Id
                          WHERE B.LotteryId = @LotteryId ORDER BY A.Sort,B.Sort";

                var lookup = new Dictionary<string,NormGroupOutput>();

                return _cacheManager.Get<ICollection<NormGroupOutput>>(redisKey, () =>
                {
                    conn.Query<NormGroupOutput, PlanInfoOutput, NormGroupOutput>(sql, (ng, pi) =>
                    {
                        NormGroupOutput p;
                        if (!lookup.TryGetValue(ng.Id,out p))
                        {
                            ng.PlanInfos = new List<PlanInfoOutput>();
                            lookup.Add(ng.Id, p = ng);
                        }

                        p.PlanInfos.Add(pi);
                        return p;
                    },new { LotteryId = lotteryId });

                    return lookup.Values;
                });
            }
        }
    }
}