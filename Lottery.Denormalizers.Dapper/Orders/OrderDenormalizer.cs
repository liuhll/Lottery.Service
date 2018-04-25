using ECommon.Dapper;
using ECommon.IO;
using ENode.Infrastructure;
using Lottery.Core.Domain.Orders;
using Lottery.Infrastructure;
using System.Threading.Tasks;

namespace Lottery.Denormalizers.Dapper.Orders
{
    public class OrderDenormalizer : AbstractDenormalizer, IMessageHandler<AddOrderRecordEvent>
    {
        public Task<AsyncTaskResult> HandleAsync(AddOrderRecordEvent evt)
        {
            return TryInsertRecordAsync(conn =>
            {
                return conn.InsertAsync(new
                {
                    Id = evt.AggregateRootId,
                    evt.SalesOrderNo,
                    evt.GoodsId,
                    evt.AmountType,
                    evt.AuthRankId,
                    evt.Count,
                    evt.LotteryId,
                    evt.Status,
                    evt.OrderCost,
                    evt.OrderSourceType,
                    evt.OriginalCost,
                    evt.UnitPrice,
                    CreateTime = evt.Timestamp
                }, TableNameConstants.OrderRecorTable);
            });
        }
    }
}