using ENode.Domain;
using System;

namespace Lottery.Core.Domain.UserRoles
{
    public class UserRole : AggregateRoot<string>
    {
        public UserRole(
          string id,
          string userId,
          string roleId,
          string createBy,
          bool? isDelete
          ) : base(id)
        {
            UserId = userId;
            RoleId = roleId;
            CreateBy = createBy;
            CreateTime = DateTime.Now;
            IsDelete = isDelete;
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; private set; }

        /// <summary>
        /// 角色表
        /// </summary>
        public string RoleId { get; private set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public bool? IsDelete { get; private set; }
    }
}