using System.Threading.Tasks;
using ECommon.Dapper;
using ECommon.IO;
using ENode.Infrastructure;
using Lottery.Core.Domain.LotteryDatas;
using Lottery.Infrastructure;

namespace Lottery.Denormalizers.Dapper
{
    public class LotteryDataDenormalizer : AbstractDenormalizer, IMessageHandler<RunNewLotteryEvent>
    {
        public Task<AsyncTaskResult> HandleAsync(RunNewLotteryEvent evnt)
        {
            return TryInsertRecordAsync(connection =>
            {
                return connection.InsertAsync(new
                {
                    Id = evnt.AggregateRootId,
                    evnt.Data,
                    evnt.LotteryId,
                    InsertTime = evnt.Timestamp,
                    evnt.Period,
                    evnt.LotteryTime,
                    ReplyCount = 0,
                    evnt.Version
                }, TableNameConstants.LotteryDataTable);
            },GetLotteryConnection());
        }
    }
}