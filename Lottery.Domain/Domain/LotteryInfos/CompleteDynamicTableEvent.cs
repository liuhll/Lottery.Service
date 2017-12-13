using ENode.Eventing;

namespace Lottery.Core.Domain.LotteryInfos
{
    public class CompleteDynamicTableEvent : DomainEvent<string>
    {
        private CompleteDynamicTableEvent()
        {
        }

        public CompleteDynamicTableEvent(bool isCompleteDynamicTable)
        {
            IsCompleteDynamicTable = isCompleteDynamicTable;
        }

        public bool IsCompleteDynamicTable { get; private set; }
    }
}