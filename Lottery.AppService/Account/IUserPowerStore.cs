using Lottery.Dtos.Power;
using System.Collections.Generic;

namespace Lottery.AppService.Account
{
    public interface IUserPowerStore
    {
        /// <summary>
        /// Gets permission grant setting informations for a user.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List of permission setting informations</returns>
        ICollection<PowerGrantInfo> GetPermissions(string userId);

        /// <summary>
        /// Checks whether a role has a permission grant setting info.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="permissionGrant">Permission grant setting info</param>
        /// <returns></returns>
        bool HasPermission(string userId, PowerGrantInfo permissionGrant);
    }
}