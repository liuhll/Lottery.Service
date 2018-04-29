using ENode.Domain;
using System;

namespace Lottery.Core.Domain.Roles
{
    public class Role : AggregateRoot<string>
    {
        public Role(
          string id,
          string roleName,
          string description,
          string createBy,
          string updateBy,
          bool? isDelete
          ) : base(id)
        {
            RoleName = roleName;
            Description = description;
            CreateBy = createBy;
            CreateTime = DateTime.Now;
            UpdateBy = updateBy;
            IsDelete = isDelete;
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; private set; }

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
        /// 修改人
        /// </summary>
        public string UpdateBy { get; private set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public bool? IsDelete { get; private set; }
    }
}