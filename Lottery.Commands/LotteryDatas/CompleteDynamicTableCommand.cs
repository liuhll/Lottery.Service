using ENode.Commanding;

namespace Lottery.Commands.LotteryDatas
{
    public class CompleteDynamicTableCommand : Command<string>
    {
        private CompleteDynamicTableCommand()
        {

        }

        public CompleteDynamicTableCommand(string id,bool isComplteDynamicTable) : base(id)
        {
            IsComplteDynamicTable = isComplteDynamicTable;
        }

        public bool IsComplteDynamicTable { get; private set; }
    }
}