using System.Collections.Generic;
using System.Linq;
using ECommon.Components;
using ECommon.Dapper;
using Lottery.Core.Caching;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Extensions;
using Lottery.QueryServices.Lotteries;

namespace Lottery.QueryServices.Dapper.Lotteries
{
    [Component]
    public class DataSiteQueryService : BaseQueryService, IDataSiteQueryService
    {
        private readonly ICacheManager _cacheManager;

        public DataSiteQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public ICollection<DataSiteDto> GetDataSites(string lotteryId)
        {
            var lotteryDataSiteKey = string.Format(RedisKeyConstants.LOTTERY_DATASITE_KEY,lotteryId.RemoveStrike());
            return _cacheManager.Get<ICollection<DataSiteDto>>(lotteryDataSiteKey, () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    return conn.QueryList<DataSiteDto>(null,TableNameConstants.DataSiteTable).ToList();
                }
            });
        }
    }
}