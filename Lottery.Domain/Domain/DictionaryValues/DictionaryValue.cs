using System;
using ENode.Domain;

namespace Lottery.Core.Domain.DictionaryValues
{
   public class DictionaryValue : AggregateRoot<string>
   {
      public DictionaryValue(
        string id,
        string dicCode,
        string value,
        string remark,
        int? sort,
        string createBy,
        string updateBy
        ) : base(id)
      {
            DicCode = dicCode;
            Value = value;
            Remark = remark;
            Sort = sort;
            CreateBy = createBy;
            CreateTime = DateTime.Now;
            UpdateBy = updateBy;
       
      }         
 
      /// <summary>
      /// 
      /// </summary>
      public string DicCode { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public string Value { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public string Remark { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public int? Sort { get; private set; }
      
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
