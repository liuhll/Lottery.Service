using System;
using ENode.Domain;

namespace Lottery.Core.Domain.PlanKeyNumbers
{
   public class PlanKeyNumber : AggregateRoot<string>
   {
      public PlanKeyNumber(
        string id,
        string planInfoId,
        string positionId,
        int? numberValue
        ) : base(id)
      {
            PlanInfoId = planInfoId;
            PositionId = positionId;
            NumberValue = numberValue;
       
      }         
 
      /// <summary>
      /// 计划Id
      /// </summary>
      public string PlanInfoId { get; private set; }
      
      /// <summary>
      /// 彩票位置Id
      /// </summary>
      public string PositionId { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public int? NumberValue { get; private set; }
      
      
   }   
}
