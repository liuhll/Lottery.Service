using System.Threading.Tasks;
using Dapper;
using ECommon.Components;
using Lottery.Core.Caching;
using Lottery.Dtos.Account;
using Lottery.Infrastructure;
using Lottery.QueryServices.UserInfos;

namespace Lottery.QueryServices.Dapper.UserInfos
{
    [Component]
    public class UserInfoService : BaseQueryService, IUserInfoService
    {
        private readonly ICacheManager _cacheManager;

        public UserInfoService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public Task<UserInfoDto> GetUserInfo(string account)
        {
            var userInfoRedisKey = string.Format(RedisKeyConstants.USERINFO_KEY, account);

            var userInfo = _cacheManager.Get<UserInfoDto>(userInfoRedisKey,  () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    var sql = "SELECT * FROM [dbo].[F_UserInfo] WHERE (UserName=@UserName OR Email=@UserName OR Phone=@UserName) AND ISDELETE=0";
                    return  conn.QueryFirstOrDefault<UserInfoDto>(sql,new { @UserName = account});
                }
            });
            return Task.FromResult(userInfo);
        }
    }
}