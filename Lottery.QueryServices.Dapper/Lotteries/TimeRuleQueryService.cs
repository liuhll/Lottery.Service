using ECommon.Components;
using ECommon.Dapper;
using Lottery.Core.Caching;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure;
using Lottery.QueryServices.Lotteries;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Lottery.QueryServices.Dapper.Lotteries
{
    [Component]
    public class TimeRuleQueryService : BaseQueryService, ITimeRuleQueryService
    {
        private readonly ICacheManager _cacheManager;

        public TimeRuleQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public ICollection<TimeRuleDto> GetTimeRules(string lotteryId)
        {
            var lotteryTimeRuleKey = string.Format(RedisKeyConstants.LOTTERY_TIME_RULE_KEY, lotteryId);

            return _cacheManager.Get<ICollection<TimeRuleDto>>(lotteryTimeRuleKey, () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    return conn.QueryList<TimeRuleDto>(new { lotteryId }, TableNameConstants.TimeRuleTable).ToList();
                }
            });
        }
    }
}