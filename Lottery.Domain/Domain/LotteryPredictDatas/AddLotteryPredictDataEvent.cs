using ENode.Eventing;

namespace Lottery.Core.Domain.LotteryPredictDatas
{
    public class AddLotteryPredictDataEvent : DomainEvent<string>
    {
        private AddLotteryPredictDataEvent()
        {

        }
        public AddLotteryPredictDataEvent(string normConfigId, int currentPredictPeriod,
            int startPeriod, int endPeriod, int minorCycle, string predictedData,
            int predictedResult, double currentScore, string createBy, string predictTable)
        {
            NormConfigId = normConfigId;
            CurrentPredictPeriod = currentPredictPeriod;
            StartPeriod = startPeriod;
            EndPeriod = endPeriod;
            MinorCycle = minorCycle;
            PredictedData = predictedData;
            PredictedResult = predictedResult;
            CurrentScore = currentScore;
            CreateBy = createBy;
            PredictTable = predictTable;

        }

        public string NormConfigId { get; private set; }

        public string PredictTable { get; private set; }

        public int CurrentPredictPeriod { get; private set; }

        public int StartPeriod { get; private set; }

        public int EndPeriod { get; private set; }

        public int MinorCycle { get; private set; }

        public string PredictedData { get; private set; }

        public int PredictedResult { get; private set; }

        public double CurrentScore { get; private set; }

        public string CreateBy { get; private set; }
    }
}