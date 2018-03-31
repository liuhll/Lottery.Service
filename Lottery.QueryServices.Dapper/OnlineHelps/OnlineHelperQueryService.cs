using System.Collections.Generic;
using System.Linq;
using Dapper;
using ECommon.Components;
using Lottery.Core.Caching;
using Lottery.Infrastructure;
using Lottery.QueryServices.OnlineHelps;

namespace Lottery.QueryServices.Dapper.OnlineHelps
{
    [Component]
    public class OnlineHelperQueryService : BaseQueryService, IOnlineHelpQueryService
    {
        private readonly ICacheManager _cacheManager;

        public OnlineHelperQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public ICollection<dynamic> GetOnlineHelps(string lotteryCode)
        {
            var sql = @"SELECT Code,HelpType,Title,Content FROM [dbo].[B_OnlineHelp]
                        where Status = 1 AND (Code='common' Or Code=@Code)
                        Order by Sort Desc, CreateTime Desc";
            var cacheKey = string.Format(RedisKeyConstants.ONLINEHELP_LOTTERY_KEY, lotteryCode);
            return _cacheManager.Get<ICollection<dynamic>>(cacheKey, () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    return conn.Query(sql, new { Code = lotteryCode }).ToList();
                }
            });
        
        }
    }
}