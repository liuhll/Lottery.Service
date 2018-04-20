using Dapper;
using ECommon.Components;
using Lottery.Core.Caching;
using Lottery.Dtos.AppInfo;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Enums;
using Lottery.QueryServices.AppInfos;
using System.ComponentModel;

namespace Lottery.QueryServices.Dapper.AppInfos
{
    [Component]
    public class AppInfoQueryService : BaseQueryService, IAppInfoQueryService
    {
        private readonly ICacheManager _cacheManager;

        public AppInfoQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public AppInfoOutput GetAppInfo(AppPlatform platform)
        {
            var cacheKey = string.Format(RedisKeyConstants.APPINFO_KEY, platform);
            return _cacheManager.Get<AppInfoOutput>(cacheKey, () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    var sql = "SELECT * FROM[dbo].[B_AppInfo] WHERE Platform = @Platform";
                    conn.Open();
                    return conn.QueryFirst<AppInfoOutput>(sql, new { Platform = platform });
                }
            });
        }
    }
}