using ENode.Commanding;

namespace Lottery.Commands.LotteryPredicts
{
    public class PredictDataCommand : Command<string>
    {
        private PredictDataCommand()
        {
        }

        public PredictDataCommand(string id, string normConfigId, int currentPredictPeriod,
            int startPeriod, int endPeriod, int minorCycle, string predictedData,
            int predictedResult, double currentScore, string createBy, string predictTable,
            string lotteryCode, bool isSwitchFormula) : base(id)
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
            LotteryCode = lotteryCode;
            IsSwitchFormula = isSwitchFormula;
        }

        public int LookupPeriodCount { get; private set; }

        public string LotteryCode { get; private set; }

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

        public bool IsSwitchFormula { get; private set; }
    }
}