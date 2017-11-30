using System;
using ENode.Domain;

namespace Lottery.Core.Domain.LotteryFinalDatas
{
   public class LotteryFinalData : AggregateRoot<string>
   {
      public LotteryFinalData(
        string id,
        string lotteryId,
        int todayFirstPeriod,
        int finalPeriod,
        int planState,
        string data,
        DateTime lotteryTime
        ) : base(id)
      {
            LotteryId = lotteryId;
            FinalPeriod = finalPeriod;
            PlanState = planState;
            Data = data;
            LotteryTime = lotteryTime;
          TodayFirstPeriod = todayFirstPeriod;


      }         
 
      /// <summary>
      /// 彩种Id
      /// </summary>
      public string LotteryId { get; private set; }
      
      /// <summary>
      /// 期数
      /// </summary>
      public int FinalPeriod { get; private set; }

      /// <summary>
      /// 今日开奖的第一期
      /// </summary>
      public int TodayFirstPeriod { get; private set; }

       /// <summary>
      /// 计划追号状态
      /// </summary>
      public int PlanState { get; private set; }
      
      /// <summary>
      /// 开奖数据
      /// </summary>
      public string Data { get; private set; }
      
      /// <summary>
      /// 开奖时间
      /// </summary>
      public DateTime LotteryTime { get; private set; }
      
      /// <summary>
      /// 更新时间
      /// </summary>
      public DateTime UpdateTime { get; private set; }
      
      
   }   
}
