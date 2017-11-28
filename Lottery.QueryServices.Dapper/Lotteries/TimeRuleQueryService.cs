using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ECommon.Components;
using ECommon.Dapper;
using Lottery.Core.Caching;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Extensions;
using Lottery.QueryServices.Lotteries;

namespace Lottery.QueryServices.Dapper.Lotteries
{
    [Component]
    public class TimeRuleQueryService : BaseQueryService, ITimeRuleQueryService
    {
        protected readonly ICacheManager _cacheManager;

        public TimeRuleQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public ICollection<TimeRuleDto> GetTimeRules(string lotteryId)
        {
            var lotteryTimeRuleKey = string.Format(RedisKeyConstants.LOTTERY_TIME_RULE_KEY, lotteryId.RemoveStrike());

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