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
    public class UserTicketService : BaseQueryService, IUserTicketService
    {
        private readonly ICacheManager _cacheManager;

        public UserTicketService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public Task<UserTicketDto> GetValidTicketInfo(string userId)
        {
            var userValidTicketKey = string.Format(RedisKeyConstants.USERINFO_TiCKET_KEY,userId);
            var ticketInfo = _cacheManager.Get<UserTicketDto>(userValidTicketKey, () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    var sql = @"SELECT TOP 1 *
                    FROM[dbo].[F_UserTicket] WHERE UserId = @UserId AND AccessToken IS NOT NULL AND AccessToken <> ''";
                    return conn.QueryFirstOrDefault<UserTicketDto>(sql, new { @UserId = userId });
                }
            });
            return Task.FromResult(ticketInfo);
        }
    }
}