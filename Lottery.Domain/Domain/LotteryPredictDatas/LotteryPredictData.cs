using System;
using ENode.Domain;

namespace Lottery.Core.Domain.LotteryPredictDatas
{
   public class LotteryPredictData : AggregateRoot<string>
   {
      public LotteryPredictData(
        string id,
        string normConfigId,
        int currentPredictPeriod,
        int? startPeriod,
        int? endPeriod,
        int? minorCycle,
        string predictedData,
        int? predictedResult,
        decimal? currentScore,
        string createBy,
        string updateBy
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
            UpdateBy = updateBy;
       
      }         
 
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
      public decimal? CurrentScore { get; private set; }
      
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
      
      
   }   
}
