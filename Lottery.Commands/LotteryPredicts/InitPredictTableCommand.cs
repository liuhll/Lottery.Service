using System.Collections.Generic;
using ENode.Commanding;

namespace Lottery.Commands.LotteryPredicts
{
    public class InitPredictTableCommand : Command<string>
    {
     

        private InitPredictTableCommand()
        {

        }

        public InitPredictTableCommand(string id,string predictDbName, IList<string> predictTableNames) : base(id)
        {
            PredictDbName = predictDbName;
            PredictTableNames = predictTableNames;

        }

        public IList<string> PredictTableNames { get; private set; }

        public string PredictDbName { get; private set; }

    }
}