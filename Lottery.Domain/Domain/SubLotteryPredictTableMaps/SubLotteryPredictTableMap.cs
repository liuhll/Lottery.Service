using System;
using ENode.Domain;

namespace Lottery.Core.Domain.SubLotteryPredictTableMaps
{
   public class SubLotteryPredictTableMap : AggregateRoot<string>
   {
      public SubLotteryPredictTableMap(
        string id,
        string planId,
        string predictDataTable,
        string predictDataTableId,
        string createBy,
        ) : base(id)
      {
            PlanId = planId;
            PredictDataTable = predictDataTable;
            PredictDataTableId = predictDataTableId;
            CreateBy = createBy;
            CreateTime = DateTime.Now;
       
      }         
 
      /// <summary>
      /// 
      /// </summary>
      public string PlanId { get; private set; }
      
      /// <summary>
      /// 计划追号结果存储表名称
      /// </summary>
      public string PredictDataTable { get; private set; }
      
      /// <summary>
      /// 上一个计划追号存储表Id
      /// </summary>
      public string PredictDataTableId { get; private set; }
      
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
