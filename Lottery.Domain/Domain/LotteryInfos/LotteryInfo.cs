﻿using System;
using ENode.Domain;

namespace Lottery.Core.Domain.LotteryInfos
{
   public class LotteryInfo : AggregateRoot<string>
   {
      public LotteryInfo(
        string id,
        string lotteryCode,
        string name,
        int? tableStrategy,
        bool? isCompleteDynamicTable,
        string createBy
        ) : base(id)
      {
            LotteryCode = lotteryCode;
            Name = name;
            TableStrategy = tableStrategy;
            IsCompleteDynamicTable = isCompleteDynamicTable;
            CreateBy = createBy;
            CreateTime = DateTime.Now;
       
      }         
 
      /// <summary>
      /// 彩种编码
      /// </summary>
      public string LotteryCode { get; private set; }
      
      /// <summary>
      /// 彩种名称
      /// </summary>
      public string Name { get; private set; }
      
      /// <summary>
      /// 分表策略;1.按月;2.按季度;3.按年
      /// </summary>
      public int? TableStrategy { get; private set; }
      
      /// <summary>
      /// 是否完成动态分表的配置
      /// </summary>
      public bool? IsCompleteDynamicTable { get; private set; }
      
      /// <summary>
      /// 创建人
      /// </summary>
      public string CreateBy { get; private set; }
      
      /// <summary>
      /// 日期
      /// </summary>
      public DateTime? CreateTime { get; private set; }
      
      
   }   
}