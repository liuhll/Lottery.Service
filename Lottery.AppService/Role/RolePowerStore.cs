using System.Collections.Generic;
using System.Linq;
using ECommon.Components;
using ECommon.Extensions;
using Lottery.Dtos.Power;
using Lottery.Dtos.RoleDto;
using Lottery.QueryServices.Powers;

namespace Lottery.AppService.Role
{
    [Component]
    public class RolePowerStore : IRolePowerStore
    {
        private readonly IRolePowerQueryService _rolePowerQueryService;

        public RolePowerStore(IRolePowerQueryService rolePowerQueryService)
        {
            _rolePowerQueryService = rolePowerQueryService;
        }


        public ICollection<PowerGrantInfo> GetPermissions(RoleDto role)
        {
            return GetPermissions(role.Id);
        }

        public ICollection<PowerGrantInfo> GetPermissions(string roleId)
        {
            return _rolePowerQueryService.GetPermissions(roleId);
        }

        public bool HasPermission(string roleId, PowerGrantInfo powerGrantInfo)
        {
            var rolePowers = GetPermissions(roleId);
            return rolePowers.Safe().FirstOrDefault(p => p.PowerCode == powerGrantInfo.PowerCode) != null;
        }

    }
}