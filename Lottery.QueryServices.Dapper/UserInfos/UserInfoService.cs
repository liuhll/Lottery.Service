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
            using (var conn = GetLotteryConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM [dbo].[F_UserInfo] WHERE (UserName=@UserName OR Email=@UserName OR Phone=@UserName) AND ISDELETE=0";
                var userInfo = conn.QueryFirstOrDefault<UserInfoDto>(sql, new { @UserName = account });
                return Task.FromResult(userInfo);
            }
           
        }

        public Task<UserInfoDto> GetUserInfoById(string id)
        {
            var userInfoRedisKey = string.Format(RedisKeyConstants.USERINFO_KEY, id);
            var userInfo = _cacheManager.Get<UserInfoDto>(userInfoRedisKey, () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    var sql = "SELECT * FROM [dbo].[F_UserInfo] WHERE Id=@Id AND ISDELETE=0";
                    return conn.QueryFirstOrDefault<UserInfoDto>(sql, new { @Id = id });
                }
            });
            return Task.FromResult(userInfo);
        }
    }
}