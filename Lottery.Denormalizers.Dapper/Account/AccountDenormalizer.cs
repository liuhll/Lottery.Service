using System.Threading.Tasks;
using ECommon.Dapper;
using ECommon.IO;
using ENode.Infrastructure;
using Lottery.Core.Caching;
using Lottery.Core.Domain.UserInfos;
using Lottery.Core.Domain.UserTicket;
using Lottery.Infrastructure;

namespace Lottery.Denormalizers.Dapper.Account
{
    public class AccountDenormalizer : AbstractDenormalizer,
        IMessageHandler<AddUserTicketEvent>,
        IMessageHandler<UpdateUserTicketEvent>,
        IMessageHandler<InvalidAccessTokenEvent>,
        IMessageHandler<AddUserInfoEvent>,
        IMessageHandler<BindUserEmailEvent>,
        IMessageHandler<BindUserPhoneEvent>,
        IMessageHandler<UpdateLoginTimeEvent>


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
                    CreateTime = evnt.Timestamp,
               
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
                }, new
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
                    InvalidTime = evnt.Timestamp,
                    UpdateTime = evnt.Timestamp
                }, new
                {
                    Id = evnt.AggregateRootId,
                }, TableNameConstants.UserTicketTable);
            });
        }

        public Task<AsyncTaskResult> HandleAsync(AddUserInfoEvent evnt)
        {
            return TryInsertRecordAsync(conn =>
            {
                var userInfoRedisKey = string.Format(RedisKeyConstants.USERINFO_KEY, evnt.AggregateRootId);
                _cacheManager.Remove(userInfoRedisKey);
                return conn.InsertAsync(new
                {
                    Id = evnt.AggregateRootId,
                    evnt.AccountRegistType,
                    evnt.ClientRegistType,
                    evnt.Email,
                    evnt.IsActive,
                    evnt.IsDelete,
                    evnt.Password,
                    evnt.Phone,
                    evnt.UserName,
                    CreateTime = evnt.Timestamp,
                    LoginClientCount = 0,
                    Balance = 0,
                    Points = evnt.Points,
                    TotalRecharge = 0,
                    TotalConsumeAccount = 0,
                    PointCount = 0,
                    AmountCount = 0
                }, TableNameConstants.UserInfoTable);
            });
        }


        public Task<AsyncTaskResult> HandleAsync(BindUserEmailEvent evnt)
        {
            return TryUpdateRecordAsync(conn =>
            {
                var userInfoKey = string.Format(RedisKeyConstants.USERINFO_KEY, evnt.AggregateRootId);
                _cacheManager.Remove(userInfoKey);
                return conn.UpdateAsync(new
                {
                    Email = evnt.Email,
                    UpdateBy = evnt.AggregateRootId,
                    UpdateTime = evnt.Timestamp
                }, new
                {
                    Id = evnt.AggregateRootId,
                }, TableNameConstants.UserInfoTable);
            });
        }

        public Task<AsyncTaskResult> HandleAsync(BindUserPhoneEvent evnt)
        {
            return TryUpdateRecordAsync(conn =>
            {
                var userInfoKey = string.Format(RedisKeyConstants.USERINFO_KEY, evnt.AggregateRootId);
                _cacheManager.Remove(userInfoKey);
                return conn.UpdateAsync(new
                {
                    Phone = evnt.Phone,
                    UpdateBy = evnt.AggregateRootId,
                    UpdateTime = evnt.Timestamp
                }, new
                {
                    Id = evnt.AggregateRootId,
                }, TableNameConstants.UserInfoTable);
            });
        }


        public Task<AsyncTaskResult> HandleAsync(UpdateLoginTimeEvent evnt)
        {
            return TryUpdateRecordAsync(conn =>
            {
                var userInfoKey = string.Format(RedisKeyConstants.USERINFO_KEY, evnt.AggregateRootId);
                _cacheManager.Remove(userInfoKey);
                return conn.UpdateAsync(new
                {
                    LastLoginTime = evnt.Timestamp
                }, new
                {
                    Id = evnt.AggregateRootId,
                }, TableNameConstants.UserInfoTable);
            });
        }


    }
}