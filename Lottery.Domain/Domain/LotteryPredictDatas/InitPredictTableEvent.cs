using ENode.Eventing;
using System.Collections.Generic;

namespace Lottery.Core.Domain.LotteryPredictDatas
{
    public class InitPredictTableEvent : DomainEvent<string>
    {
        private InitPredictTableEvent()
        {
        }

        public InitPredictTableEvent(IList<string> predictTableNames, string lotteryCode, string predictDbName)
        {
            PredictTableNames = predictTableNames;
            PredictDbName = predictDbName;
            LotteryCode = lotteryCode;
        }

        public string LotteryCode { get; private set; }

        public IList<string> PredictTableNames { get; private set; }

        public string PredictDbName { get; private set; }
    }
}