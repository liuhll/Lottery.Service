using Dapper;
using ECommon.Components;
using Lottery.Core.Caching;
using Lottery.Dtos.IdentifyCodes;
using Lottery.Infrastructure;
using Lottery.QueryServices.IdentifyCodes;

namespace Lottery.QueryServices.Dapper.IdentifyCodes
{
    [Component]
    public class IdentifyCodeQueryService : BaseQueryService, IIdentifyCodeQueryService
    {
        private readonly ICacheManager _cacheManager;

        public IdentifyCodeQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public IdentifyCodeDto GetIdentifyCode(string receiver)
        {
            using (var conn = GetLotteryConnection())
            {
                conn.Open();
                var cacheKey = string.Format(RedisKeyConstants.IDENTIFY_CODE_USER_KEY, receiver);
                return _cacheManager.Get<IdentifyCodeDto>(cacheKey, () =>
                {
                    var sql = "SELECT TOP 1 * FROM [dbo].[B_IdentifyCode] WHERE Receiver=@Receiver AND Status=0";
                    return conn.QueryFirstOrDefault<IdentifyCodeDto>(sql, new { @Receiver = receiver });
                });
            }
        }
    }
}