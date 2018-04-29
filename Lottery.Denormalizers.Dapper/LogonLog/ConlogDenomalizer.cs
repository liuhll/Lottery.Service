using ECommon.Dapper;
using ECommon.IO;
using ENode.Infrastructure;
using Lottery.Core.Domain.LogonLog;
using Lottery.Infrastructure;
using System.Threading.Tasks;

namespace Lottery.Denormalizers.Dapper.LogonLog
{
    public class ConlogDenomalizer : AbstractDenormalizer,
        IMessageHandler<AddConLogEvent>,
        IMessageHandler<LogoutEvent>,
        IMessageHandler<UpdateTokenEvent>
    {
        public Task<AsyncTaskResult> HandleAsync(AddConLogEvent evnt)
        {
            return TryInsertRecordAsync(conn =>
            {
                return conn.InsertAsync(new
                {
                    Id = evnt.AggregateRootId,
                    evnt.UserId,
                    evnt.SystemTypeId,
                    evnt.Ip,
                    InvalidTime = evnt.InvalidTime,
                    evnt.ClientNo,
                    LoginTime = evnt.Timestamp,
                    UpdateTokenCount = evnt.UpdateTokenCount,
                    Createby = evnt.UserId,
                    CreateTime = evnt.Timestamp,
                }, TableNameConstants.ConLogTable);
            });
        }

        public Task<AsyncTaskResult> HandleAsync(UpdateTokenEvent evnt)
        {
            return TryUpdateRecordAsync(conn =>
            {
                return conn.UpdateAsync(new
                {
                    evnt.UpdateTokenCount,
                    evnt.InvalidTime,
                    Updateby = evnt.UpdateBy,
                    UpdateTime = evnt.Timestamp,
                }, new { Id = evnt.AggregateRootId, }, TableNameConstants.ConLogTable);
            });
        }

        public Task<AsyncTaskResult> HandleAsync(LogoutEvent evnt)
        {
            return TryUpdateRecordAsync(conn =>
            {
                return conn.UpdateAsync(new
                {
                    LogoutTime = evnt.LogoutTime,
                    OnlineTime = evnt.OnlineTime,
                    Updateby = evnt.UserId,
                    UpdateTime = evnt.Timestamp,
                }, new { Id = evnt.AggregateRootId, }, TableNameConstants.ConLogTable);
            });
        }
    }
}