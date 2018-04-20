using ENode.Domain;
using System;

namespace Lottery.Core.Domain.Powers
{
    public class Power : AggregateRoot<string>
    {
        public Power(
          string id,
          string powerCode,
          string powerName,
          string parentId,
          string url,
          string powerType,
          string description,
          string createBy,
          string updateBy,
          bool? isDelete
          ) : base(id)
        {
            PowerCode = powerCode;
            PowerName = powerName;
            ParentId = parentId;
            Url = url;
            PowerType = powerType;
            Description = description;
            CreateBy = createBy;
            CreateTime = DateTime.Now;
            UpdateBy = updateBy;
            IsDelete = isDelete;
        }

        /// <summary>
        /// 权限编码
        /// </summary>
        public string PowerCode { get; private set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string PowerName { get; private set; }

        /// <summary>
        /// 父权限ID
        /// </summary>
        public string ParentId { get; private set; }

        /// <summary>
        /// URI
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public string PowerType { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; private set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateBy { get; private set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public bool? IsDelete { get; private set; }
    }
}