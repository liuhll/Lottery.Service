using ENode.Domain;
using System;

namespace Lottery.Core.Domain.LotteryPredictDatas
{
    public class LotteryPredictData : AggregateRoot<string>
    {
        public LotteryPredictData(
          string id,
          string normConfigId,
          int currentPredictPeriod,
          int startPeriod,
          int endPeriod,
          int minorCycle,
          string predictedData,
          int predictedResult,
          double currentScore,
          string createBy,
          string predictTable,
          string lotteryCode,
          bool isSwitchFormula
          ) : base(id)
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
            CreateTime = DateTime.Now;
            PredictTable = predictTable;
            LotteryCode = lotteryCode;
            if (isSwitchFormula)
            {
                // :todo 切换公式
            }
            else
            {
                ApplyEvent(new AddLotteryPredictDataEvent(normConfigId, currentPredictPeriod, startPeriod, endPeriod, minorCycle,
                    predictedData, predictedResult, currentScore, createBy, predictTable, lotteryCode));
            }
        }

        public string LotteryCode { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public string NormConfigId { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public int CurrentPredictPeriod { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public int? StartPeriod { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public int? EndPeriod { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public int? MinorCycle { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public string PredictedData { get; private set; }

        /// <summary>
        /// 1.正确;2.错误,3,等待开奖
        /// </summary>
        public int? PredictedResult { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public double? CurrentScore { get; private set; }

        public string PredictTable { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public string CreateBy { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime? CreateTime { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public string UpdateBy { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime? UpdateTime { get; private set; }

        #region Handle Methods

        private void Handle(AddLotteryPredictDataEvent evnt)
        {
            NormConfigId = evnt.NormConfigId;
            CurrentPredictPeriod = evnt.CurrentPredictPeriod;
            StartPeriod = evnt.StartPeriod;
            EndPeriod = evnt.EndPeriod;
            MinorCycle = evnt.MinorCycle;
            PredictedData = evnt.PredictedData;
            PredictedResult = evnt.PredictedResult;
            CurrentScore = evnt.CurrentScore;
            CreateBy = evnt.CreateBy;
            PredictTable = evnt.PredictTable;
            LotteryCode = evnt.LotteryCode;
        }

        #endregion Handle Methods
    }
}