using System;
using ENode.Domain;

namespace Lottery.Core.Domain.NormConfigs
{
   public class NormConfig : AggregateRoot<string>
   {
      public NormConfig(
        string id,
        string userId,
        string lotteryId,
        string planId,
        int planCycle,
        int forecastCount,
        int unitHistoryCount,
        int? lastStartPeriod,
        int? maxRightSeries,
        int? maxErrortSeries,
        int? expectScore,
        bool? isEnable,
        bool? isDefualt,
        string createBy,
        string updateBy
        ) : base(id)
      {
            UserId = userId;
            PlanId = planId;
            LotteryId = lotteryId;
            PlanCycle = planCycle;
            ForecastCount = forecastCount;
            UnitHistoryCount = unitHistoryCount;
            LastStartPeriod = lastStartPeriod;
            MaxRightSeries = maxRightSeries;
            MaxErrortSeries = maxErrortSeries;
            ExpectScore = expectScore;
            IsEnable = isEnable;
            IsDefualt = isDefualt;
            CreateBy = createBy;
            CreateTime = DateTime.Now;
            UpdateBy = updateBy;
       
      }         
 
      /// <summary>
      /// 
      /// </summary>
      public string UserId { get; private set; }

       public string LotteryId { get; private set; }

       /// <summary>
      /// 
      /// </summary>
      public string PlanId { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public int PlanCycle { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public int ForecastCount { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public int UnitHistoryCount { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public int? LastStartPeriod { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public int? MaxRightSeries { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public int? MaxErrortSeries { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public int? ExpectScore { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public bool? IsEnable { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public bool? IsDefualt { get; private set; }
      
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
