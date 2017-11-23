using System;
using ENode.Domain;

namespace Lottery.Core.Domain.PlanInfos
{
   public class PlanInfo : AggregateRoot<string>
   {
      public PlanInfo(
        string id,
        string normGroupId,
        string planCode,
        string planNormTable,
        string planName,
        string predictCode,
        bool dsType,
        int? sort
        ) : base(id)
      {
            NormGroupId = normGroupId;
            PlanCode = planCode;
            PlanNormTable = planNormTable;
            PlanName = planName;
            PredictCode = predictCode;
            DsType = dsType;
            Sort = sort;
       
      }         
 
      /// <summary>
      /// 所属组
      /// </summary>
      public string NormGroupId { get; private set; }
      
      /// <summary>
      /// 计划编码
      /// </summary>
      public string PlanCode { get; private set; }
      
      /// <summary>
      /// 计划追号指标分表名称
      /// </summary>
      public string PlanNormTable { get; private set; }
      
      /// <summary>
      /// 计划名称
      /// </summary>
      public string PlanName { get; private set; }
      
      /// <summary>
      /// 预测类型
      /// </summary>
      public string PredictCode { get; private set; }
      
      /// <summary>
      /// 0.杀码；1.定码
      /// </summary>
      public bool DsType { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public int? Sort { get; private set; }
      
      
   }   
}
