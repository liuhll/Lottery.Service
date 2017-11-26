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
        DateTime? lotteryTime
        ) : base(id)
      {
            Period = period;
            LotteryId = lotteryId;
            Data = data;
            LotteryTime = lotteryTime;

            ApplyEvent(new RunNewLotteryEvent(this));
       
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

       private void Handle(RunNewLotteryEvent evnt)
       {
           Period = evnt.Period;
           LotteryId = evnt.LotteryId;
           Data = evnt.Data;
           InsertTime = evnt.InsertTime;
           LotteryTime = evnt.LotteryTime;
       }
   }   
}
