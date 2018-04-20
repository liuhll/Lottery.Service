using ENode.Commanding;
using Lottery.Commands.Sells;
using Lottery.Core.Domain.Orders;

namespace Lottery.CommandHandlers
{
    public class OrderCommandHandler : ICommandHandler<AddOrderRecordCommand>
    {
        public void Handle(ICommandContext context, AddOrderRecordCommand command)
        {
            context.Add(new OrderRecord(command.Id, command.SalesOrderNo, command.AuthRankId, command.LotteryId, command.OrderSourceType, command.Count,
                command.UnitPrice, command.OriginalCost, command.OrderCost, command.AmountType, command.CreateBy));
        }
    }
}