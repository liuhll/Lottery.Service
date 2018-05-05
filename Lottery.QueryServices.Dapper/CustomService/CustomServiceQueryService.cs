using System.Linq;
using ECommon.Components;
using ECommon.Dapper;
using Lottery.Core.Caching;
using Lottery.Dtos.CustomService;
using Lottery.Infrastructure;
using Lottery.QueryServices.CustomService;

namespace Lottery.QueryServices.Dapper.CustomService
{
    [Component]
    public class CustomServiceQueryService : BaseQueryService, ICustomServiceQueryService
    {
        private readonly ICacheManager _cacheManager;

        public CustomServiceQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public CustomServiceOutput GetCustomService(string lotteryId)
        {
            var cacheKey = "Lottery.CustomService";
            return _cacheManager.Get<CustomServiceOutput>(cacheKey, () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    return conn.QueryList<CustomServiceOutput>(new { lotteryId }, TableNameConstants.CustomServiceTable)
                        .FirstOrDefault();
                }
            });
        }
    }
}