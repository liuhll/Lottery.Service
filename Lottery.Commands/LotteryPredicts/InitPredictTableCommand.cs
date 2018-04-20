using ENode.Commanding;
using System.Collections.Generic;

namespace Lottery.Commands.LotteryPredicts
{
    public class InitPredictTableCommand : Command<string>
    {
        private InitPredictTableCommand()
        {
        }

        public InitPredictTableCommand(string id, string predictDbName, string lotteryCode, IList<string> predictTableNames) : base(id)
        {
            LotteryCode = lotteryCode;
            PredictDbName = predictDbName;
            PredictTableNames = predictTableNames;
        }

        public string LotteryCode { get; private set; }
        public IList<string> PredictTableNames { get; private set; }

        public string PredictDbName { get; private set; }
    }
}