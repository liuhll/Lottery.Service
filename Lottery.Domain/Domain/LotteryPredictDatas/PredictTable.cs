using ENode.Domain;
using System.Collections.Generic;

namespace Lottery.Core.Domain.LotteryPredictDatas
{
    public class PredictTable : AggregateRoot<string>
    {
        public PredictTable(string id, string predictDbName, string lotteryCode, IList<string> predictTableNames) : base(id)
        {
            LotteryCode = lotteryCode;
            PredictTableNames = predictTableNames;
            PredictDbName = predictDbName;

            ApplyEvent(new InitPredictTableEvent(PredictTableNames, LotteryCode, PredictDbName));
        }

        public string LotteryCode { get; private set; }

        public IList<string> PredictTableNames { get; private set; }

        public string PredictDbName { get; private set; }

        #region Handle Methods

        private void Handle(InitPredictTableEvent evnt)
        {
            PredictDbName = evnt.PredictDbName;
            PredictTableNames = evnt.PredictTableNames;
            LotteryCode = evnt.LotteryCode;
        }

        #endregion Handle Methods

    }
}