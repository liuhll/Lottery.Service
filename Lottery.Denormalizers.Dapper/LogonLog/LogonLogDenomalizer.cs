using System.Threading.Tasks;
using ECommon.Dapper;
using ECommon.IO;
using ENode.Infrastructure;
using Lottery.Core.Domain.LogonLog;
using Lottery.Infrastructure;

namespace Lottery.Denormalizers.Dapper.LogonLog
{
    public class LogonLogDenomalizer : AbstractDenormalizer, 
        IMessageHandler<AddLogonLogEvent>,
        IMessageHandler<LogoutEvent>,
        IMessageHandler<UpdateTokenEvent>
    {
        public Task<AsyncTaskResult> HandleAsync(AddLogonLogEvent evnt)
        {
            return TryInsertRecordAsync(conn =>
            {

                return conn.InsertAsync(new
                {
                    Id = evnt.AggregateRootId,
                    UserId = evnt.UserId,
                    LoginTime = evnt.Timestamp,
                    UpdateTokenCount = 1,
                    Createby = evnt.UserId,
                    CreateTime = evnt.Timestamp,

                },TableNameConstants.LogonLogTable);
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

                }, new {Id = evnt.AggregateRootId,}, TableNameConstants.LogonLogTable);
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

                }, new { Id = evnt.AggregateRootId, }, TableNameConstants.LogonLogTable);
            });
        }
    }
}