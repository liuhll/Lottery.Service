using ENode.Domain;
using System;

namespace Lottery.Core.Domain.RolePowers
{
    public class RolePower : AggregateRoot<string>
    {
        public RolePower(
          string id,
          string roleId,
          string powerId,
          string createBy,
          bool? isDelete
          ) : base(id)
        {
            RoleId = roleId;
            PowerId = powerId;
            CreateBy = createBy;
            CreateTime = DateTime.Now;
            IsDelete = isDelete;
        }

        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; private set; }

        /// <summary>
        /// 权限Id
        /// </summary>
        public string PowerId { get; private set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime CreateTime { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public bool? IsDelete { get; private set; }
    }
}