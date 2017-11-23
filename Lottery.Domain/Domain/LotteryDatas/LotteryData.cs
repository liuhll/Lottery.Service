using System;
using ENode.Domain;

namespace Lottery.Core.Domain.LotteryDatas
{
   public class LotteryData : AggregateRoot<string>
   {
      public LotteryData(
        string id,
        int period,
        string lotteryId,
        string data,
        DateTime? insertTime,
        DateTime? lotteryTime
        ) : base(id)
      {
            Period = period;
            LotteryId = lotteryId;
            Data = data;
            InsertTime = insertTime;
            LotteryTime = lotteryTime;
       
      }         
 
      /// <summary>
      /// 
      /// </summary>
      public int Period { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public string LotteryId { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public string Data { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public DateTime? InsertTime { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public DateTime? LotteryTime { get; private set; }
      
      
   }   
}
