using System.Threading.Tasks;
using ECommon.Dapper;
using ECommon.IO;
using ENode.Infrastructure;
using Lottery.Core.Caching;
using Lottery.Core.Domain.UserTicket;
using Lottery.Infrastructure;

namespace Lottery.Denormalizers.Dapper.Account
{
    public class AccountDenormalizer : AbstractDenormalizer,
        IMessageHandler<AddUserTicketEvent>,
        IMessageHandler<UpdateUserTicketEvent>,
        IMessageHandler<InvalidAccessTokenEvent>

    {
        private readonly ICacheManager _cacheManager;

        public AccountDenormalizer(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public Task<AsyncTaskResult> HandleAsync(AddUserTicketEvent evnt)
        {
            return TryInsertRecordAsync(conn =>
            {
                var userTicketKey = string.Format(RedisKeyConstants.USERINFO_TiCKET_KEY, evnt.UserId);
                _cacheManager.Remove(userTicketKey);
                return conn.InsertAsync(new
                {
                    Id = evnt.AggregateRootId,
                    evnt.AccessToken,
                    evnt.CreateBy,
                    evnt.UserId,
                    CreateTime = evnt.Timestamp
                }, TableNameConstants.UserTicketTable);
            });
        }

        public Task<AsyncTaskResult> HandleAsync(UpdateUserTicketEvent evnt)
        {
            return TryUpdateRecordAsync(conn =>
            {
                var userTicketKey = string.Format(RedisKeyConstants.USERINFO_TiCKET_KEY, evnt.UserId);
                _cacheManager.Remove(userTicketKey);
                return conn.UpdateAsync(new
                {
                   
                    evnt.AccessToken,
                    evnt.UpdateBy,
                    evnt.UserId,
                    UpdateTime = evnt.Timestamp
                },new
                {
                    Id = evnt.AggregateRootId,
                }, TableNameConstants.UserTicketTable);
            });
        }

        public Task<AsyncTaskResult> HandleAsync(InvalidAccessTokenEvent evnt)
        {
            return TryUpdateRecordAsync(conn =>
            {
                var userTicketKey = string.Format(RedisKeyConstants.USERINFO_TiCKET_KEY, evnt.UserId);
                _cacheManager.Remove(userTicketKey);
                return conn.UpdateAsync(new
                {

                    evnt.AccessToken,
                    UpdateBy = evnt.UserId,
                    evnt.UserId,
                    UpdateTime = evnt.Timestamp
                }, new
                {
                    Id = evnt.AggregateRootId,
                }, TableNameConstants.UserTicketTable);
            });
        }
    }
}