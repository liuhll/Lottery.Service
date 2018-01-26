using System.Threading.Tasks;
using ECommon.Dapper;
using ECommon.IO;
using ENode.Infrastructure;
using Lottery.Core.Domain.LogonLog;
using Lottery.Infrastructure;

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
                    evnt.InvalidTime,
                    evnt.ClientNo,
                    LoginTime = evnt.Timestamp,
                    UpdateTokenCount = 1,
                    Createby = evnt.UserId,
                    CreateTime = evnt.Timestamp,

                },TableNameConstants.ConLogTable);
            });
        }

        public Task<AsyncTaskResult> HandleAsync(UpdateTokenEvent evnt)
        {
            return TryUpdateRecordAsync(conn =>
            {

                return conn.UpdateAsync(new
                {

                    UserId = evnt.UserId,
                    LoginTime = evnt.Timestamp,
                    UpdateTokenCount = 1,
                    Updateby = evnt.UserId,
                    UpdateTime = evnt.Timestamp,

                }, new {Id = evnt.AggregateRootId,}, TableNameConstants.ConLogTable);
            });
        }

        public Task<AsyncTaskResult> HandleAsync(LogoutEvent evnt)
        {
            return TryUpdateRecordAsync(conn =>
            {

                return conn.UpdateAsync(new
                {
                    UserId = evnt.UserId,
                    LogoutTime = evnt.Timestamp,
                    UpdateTokenCount = 1,
                    OnlineTime = (int)(evnt.Timestamp - evnt.LoginTime).TotalMinutes,
                    Updateby = evnt.UserId,
                    UpdateTime = evnt.Timestamp,

                }, new { Id = evnt.AggregateRootId, }, TableNameConstants.ConLogTable);
            });
        }
    }
}