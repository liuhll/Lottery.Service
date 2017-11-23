using System;
using ENode.Domain;

namespace Lottery.Core.Domain.DictionaryCatalogues
{
   public class DictionaryCatalogue : AggregateRoot<string>
   {
      public DictionaryCatalogue(
        string id,
        string code,
        string describe,
        bool? isDefault,
        string createBy,
        string updateBy
        ) : base(id)
      {
            Code = code;
            Describe = describe;
            IsDefault = isDefault;
            CreateBy = createBy;
            CreateTime = DateTime.Now;
            UpdateBy = updateBy;
       
      }         
 
      /// <summary>
      /// 
      /// </summary>
      public string Code { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public string Describe { get; private set; }
      
      /// <summary>
      /// 
      /// </summary>
      public bool? IsDefault { get; private set; }
      
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
