using Dapper;
using ECommon.Components;
using Lottery.Core.Caching;
using Lottery.Dtos.Norms;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Extensions;
using Lottery.QueryServices.Norms;
using System.ComponentModel;

namespace Lottery.QueryServices.Dapper.Norms
{
    [Component]
    public class NormPlanConfigQueryService : BaseQueryService, INormPlanConfigQueryService
    {
        private readonly ICacheManager _cacheManager;

        public NormPlanConfigQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public NormPlanConfigDto GetNormPlanDefaultConfig(string lotteryCode, string predictCode = null)
        {
            var redisKey = RedisKeyConstants.NORM_PLAN_CONFIG_KEY;
            if (predictCode.IsNullOrEmpty())
            {
                redisKey = string.Format(redisKey, lotteryCode, "Default");
            }
            else
            {
                redisKey = string.Format(redisKey, lotteryCode, predictCode);
            }

            return _cacheManager.Get<NormPlanConfigDto>(redisKey, () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    var sql = string.Empty;
                    if (predictCode.IsNullOrEmpty())
                    {
                        sql = @"SELECT TOP 1 * FROM [dbo].[L_NormPlanConfig] WHERE LotteryCode=@LotteryCode AND PredictCode IS NULL";
                    }
                    else
                    {
                        sql = @"SELECT TOP 1 * FROM [dbo].[L_NormPlanConfig] WHERE LotteryCode=@LotteryCode AND PredictCode=@PredictCode";
                    }
                    conn.Open();
                    return conn.QueryFirstOrDefault<NormPlanConfigDto>(sql, new
                    {
                        LotteryCode = lotteryCode,
                        PredictCode = predictCode
                    });
                }
            });
        }
    }
}