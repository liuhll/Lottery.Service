using ECommon.Dapper;
using ECommon.IO;
using ENode.Infrastructure;
using Lottery.Core.Domain.MessageRecords;
using Lottery.Infrastructure;
using System.Threading.Tasks;

namespace Lottery.Denormalizers.Dapper.Messages
{
    public class MessageDenormalizer : AbstractDenormalizer, IMessageHandler<AddMessageRecordEvent>
    {
        public Task<AsyncTaskResult> HandleAsync(AddMessageRecordEvent evnt)
        {
            return TryInsertRecordAsync(conn =>
            {
                return conn.InsertAsync(new
                {
                    Id = evnt.AggregateRootId,
                    evnt.CreateBy,
                    evnt.MessageType,
                    evnt.Receiver,
                    evnt.Sender,
                    evnt.SenderPlatform,
                    evnt.Title,
                    evnt.Content,
                    CreateTime = evnt.Timestamp,
                }, TableNameConstants.MessageRecordTable);
            });
        }
    }
}