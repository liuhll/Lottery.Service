using System;
using ENode.Domain;

namespace Lottery.Core.Domain.TimeRules
{
   public class TimeRule : AggregateRoot<string>
   {
      public TimeRule(
        string id,
        string lotteryId,
        string weekday,
        System.TimeSpan? startTime,
        System.TimeSpan? endTime,
        System.TimeSpan? tick,
        string createBy
        ) : base(id)
      {
            LotteryId = lotteryId;
            Weekday = weekday;
            StartTime = startTime;
            EndTime = endTime;
            Tick = tick;
            CreateBy = createBy;
            CreateTime = DateTime.Now;
       
      }         
 
      /// <summary>
      /// 彩种Id
      /// </summary>
      public string LotteryId { get; private set; }
      
      /// <summary>
      /// 星期
      /// </summary>
      public string Weekday { get; private set; }
      
      /// <summary>
      /// 开奖开始时间
      /// </summary>
      public System.TimeSpan? StartTime { get; private set; }
      
      /// <summary>
      /// 开奖结束时间
      /// </summary>
      public System.TimeSpan? EndTime { get; private set; }
      
      /// <summary>
      /// 时间间隔
      /// </summary>
      public System.TimeSpan? Tick { get; private set; }
      
      /// <summary>
      /// 创建人
      /// </summary>
      public string CreateBy { get; private set; }
      
      /// <summary>
      /// 创建日期
      /// </summary>
      public DateTime? CreateTime { get; private set; }
      
      
   }   
}
