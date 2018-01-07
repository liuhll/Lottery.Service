
using System.Threading.Tasks;
using ECommon.Dapper;
using ECommon.IO;
using ENode.Infrastructure;
using Lottery.Core.Caching;
using Lottery.Core.Domain.UserInfos;
using Lottery.Infrastructure;

namespace Lottery.Denormalizers.Dapper.UserInfo
{
    public class UserInfoDenormalizer : AbstractDenormalizer, 
        IMessageHandler<BindUserEmailEvent>,
        IMessageHandler<BindUserPhoneEvent>,
        IMessageHandler<UpdateLoginTimeEvent>

    {
        private readonly ICacheManager _cacheManager;

        public UserInfoDenormalizer(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
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