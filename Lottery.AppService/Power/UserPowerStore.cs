﻿using System.Collections.Generic;
using System.Linq;
using ECommon.Components;
using ECommon.Extensions;
using Lottery.Dtos.Power;
using Lottery.QueryServices.Powers;

namespace Lottery.AppService.Power
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