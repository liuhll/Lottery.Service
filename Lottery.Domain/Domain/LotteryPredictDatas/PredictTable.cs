﻿using System.Collections.Generic;
using ENode.Domain;

namespace Lottery.Core.Domain.LotteryPredictDatas
{
    public class PredictTable : AggregateRoot<string>
    {


        public PredictTable(string id,string predictDbName, IList<string> predictTableNames) : base(id)
        {

            PredictTableNames = predictTableNames;
            PredictDbName = predictDbName;

            ApplyEvent(new InitPredictTableEvent(PredictTableNames, PredictDbName));

            
        }

        public IList<string> PredictTableNames { get; private set; }

        public string PredictDbName { get; private set; }


        #region Handle Methods

        private void Handle(InitPredictTableEvent evnt)
        {
            PredictDbName = evnt.PredictDbName;
            PredictTableNames = evnt.PredictTableNames;
        }

        #endregion 
    }
}