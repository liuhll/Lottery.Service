using System;
using System.Threading.Tasks;
using ECommon.Dapper;
using ECommon.IO;
using ENode.Infrastructure;
using Lottery.Core.Domain.OpinionRecords;
using Lottery.Infrastructure;

namespace Lottery.Denormalizers.Dapper.OpinionRecords
{
    public class OpinionRecordDenormalizer : AbstractDenormalizer, IMessageHandler<AddOpinionRecordEvent>
    {
        public Task<AsyncTaskResult> HandleAsync(AddOpinionRecordEvent evt)
        {
            return TryInsertRecordAsync(conn =>
            {

                return conn.InsertAsync(new
                {
                    Id = evt.AggregateRootId,
                    evt.ContactWay,
                    evt.Content,
                    evt.CreateBy,
                    evt.OpinionType,
                    evt.Platform,
                    evt.Status,
                    CreateTime = evt.Timestamp
                }, TableNameConstants.OpinionRecordTable);
            });
        }
    }
}
