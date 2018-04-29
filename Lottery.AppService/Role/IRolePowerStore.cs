using Lottery.Dtos.Power;
using Lottery.Dtos.RoleDto;
using System.Collections.Generic;

namespace Lottery.AppService.Role
{
    public interface IRolePowerStore
    {
        /// <summary>
        /// Gets permission grant setting informations for a role.
        /// </summary>
        /// <param name="role">Role</param>
        /// <returns>List of permission setting informations</returns>
        ICollection<PowerGrantInfo> GetPermissions(RoleDto role);

        /// <summary>
        /// Gets permission grant setting informations for a role.
        /// </summary>
        /// <param name="roleId">Role id</param>
        /// <returns>List of permission setting informations</returns>
        ICollection<PowerGrantInfo> GetPermissions(string roleId);

        /// <summary>
        /// Checks whether a role has a permission grant setting info.
        /// </summary>
        /// <param name="roleId">Role id</param>
        /// <param name="powerGrantInfo">Permission grant setting info</param>
        /// <returns></returns>
        bool HasPermission(string roleId, PowerGrantInfo powerGrantInfo);
    }
}