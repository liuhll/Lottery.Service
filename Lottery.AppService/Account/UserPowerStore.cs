using ECommon.Components;
using ECommon.Extensions;
using Lottery.Dtos.Power;
using Lottery.QueryServices.Powers;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.AppService.Account
{
    [Component]
    public class UserPowerStore : IUserPowerStore
    {
        private readonly IUserPowerQueryService _userPowerQueryService;

        public UserPowerStore(IUserPowerQueryService userPowerQueryService)
        {
            _userPowerQueryService = userPowerQueryService;
        }

        public ICollection<PowerGrantInfo> GetPermissions(string userId)
        {
            return _userPowerQueryService.GetPermissions(userId);
        }

        public bool HasPermission(string userId, PowerGrantInfo permissionGrant)
        {
            var userPowers = GetPermissions(userId);
            return userPowers.Safe().FirstOrDefault(p => p.PowerCode == permissionGrant.PowerCode) != null;
        }
    }
}