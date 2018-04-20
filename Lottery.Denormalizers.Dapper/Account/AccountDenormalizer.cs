using ECommon.Dapper;
using ECommon.IO;
using ENode.Infrastructure;
using Lottery.Core.Caching;
using Lottery.Core.Domain.UserInfos;
using Lottery.Infrastructure;
using System.Threading.Tasks;

namespace Lottery.Denormalizers.Dapper.Account
{
    public class AccountDenormalizer : AbstractDenormalizer,
        IMessageHandler<AddUserInfoEvent>,
        IMessageHandler<BindUserEmailEvent>,
        IMessageHandler<BindUserPhoneEvent>,
        IMessageHandler<UpdateLoginTimeEvent>,
        IMessageHandler<UpdateUserLoginClientCountEvent>,
        IMessageHandler<UpdatePasswordEvent>

    {
        private readonly ICacheManager _cacheManager;

        public AccountDenormalizer(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public Task<AsyncTaskResult> HandleAsync(UpdateUserLoginClientCountEvent evnt)
        {
            return TryUpdateRecordAsync(conn =>
            {
                var userInfoKey = string.Format(RedisKeyConstants.USERINFO_KEY, evnt.AggregateRootId);
                _cacheManager.Remove(userInfoKey);
                return conn.UpdateAsync(new
                {
                    LoginClientCount = evnt.LoginClientCount,
                    UpdateBy = evnt.AggregateRootId,
                    UpdateTime = evnt.Timestamp
                }, new
                {
                    Id = evnt.AggregateRootId,
                }, TableNameConstants.UserInfoTable);
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
                    Balance = evnt.Balance,
                    Points = evnt.Points,
                    TotalRecharge = evnt.TotalRecharge,
                    TotalConsumeAccount = evnt.TotalConsumeAccount,
                    PointCount = evnt.PointCount,
                    AmountCount = evnt.AmountCount,
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

        public Task<AsyncTaskResult> HandleAsync(UpdatePasswordEvent evnt)
        {
            return TryUpdateRecordAsync(conn =>
            {
                var userInfoKey = string.Format(RedisKeyConstants.USERINFO_KEY, evnt.AggregateRootId);
                _cacheManager.Remove(userInfoKey);
                return conn.UpdateAsync(new
                {
                    evnt.Password,
                    evnt.UpdateBy,
                    UpdateTime = evnt.Timestamp
                }, new
                {
                    Id = evnt.AggregateRootId,
                }, TableNameConstants.UserInfoTable);
            });
        }
    }
}