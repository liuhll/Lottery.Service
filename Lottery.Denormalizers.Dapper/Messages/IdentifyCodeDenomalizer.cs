using ECommon.Dapper;
using ECommon.IO;
using ENode.Infrastructure;
using Lottery.Core.Caching;
using Lottery.Core.Domain.IdentifyCode;
using Lottery.Infrastructure;
using System.Threading.Tasks;

namespace Lottery.Denormalizers.Dapper.Messages
{
    public class IdentifyCodeDenomalizer : AbstractDenormalizer, IMessageHandler<AddIdentifyCodeEvent>, IMessageHandler<UpdateIdentifyCodeEvent>, IMessageHandler<InvalidIdentifyCodeEvent>
    {
        private readonly ICacheManager _cacheManager;

        public IdentifyCodeDenomalizer(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public Task<AsyncTaskResult> HandleAsync(AddIdentifyCodeEvent evt)
        {
            return TryInsertRecordAsync(conn =>
            {
                var cacheKey = string.Format(RedisKeyConstants.IDENTIFY_CODE_USER_KEY, evt.Receiver);
                _cacheManager.Remove(cacheKey);
                return conn.InsertAsync(new
                {
                    Id = evt.AggregateRootId,
                    evt.Code,
                    evt.IdentifyCodeType,
                    evt.MessageType,
                    evt.Receiver,
                    evt.ExpirationDate,
                    evt.Status,
                    evt.CreateBy,
                    CreateTime = evt.Timestamp
                }, TableNameConstants.IdentifyCodeTable);
            });
        }

        public Task<AsyncTaskResult> HandleAsync(UpdateIdentifyCodeEvent evt)
        {
            return TryUpdateRecordAsync(conn =>
            {
                var cacheKey = string.Format(RedisKeyConstants.IDENTIFY_CODE_USER_KEY, evt.Receiver);
                _cacheManager.Remove(cacheKey);
                return conn.UpdateAsync(new
                {
                    evt.Code,
                    evt.ExpirationDate,
                    evt.UpdateBy,
                    UpdateTime = evt.Timestamp,
                }, new
                {
                    Id = evt.AggregateRootId
                }, TableNameConstants.IdentifyCodeTable);
            });
        }

        public Task<AsyncTaskResult> HandleAsync(InvalidIdentifyCodeEvent evt)
        {
            return TryUpdateRecordAsync(conn =>
            {
                var cacheKey = string.Format(RedisKeyConstants.IDENTIFY_CODE_USER_KEY, evt.Receiver);
                _cacheManager.Remove(cacheKey);
                return conn.UpdateAsync(new
                {
                    evt.Status,
                    evt.ValidateDate,
                    evt.UpdateBy,
                    UpdateTime = evt.Timestamp
                }, new
                {
                    Id = evt.AggregateRootId
                }, TableNameConstants.IdentifyCodeTable);
            });
        }
    }
}