using System.Collections.Generic;

namespace Lottery.Dtos.Power
{
    public class UserPowerCacheItem
    {
        public const string CacheStoreName = "LotteryUserPowers";

        public string UserId { get; set; }

        public List<string> RoleIds { get; set; }

        public HashSet<string> GrantedPermissions { get; set; }

        public HashSet<string> ProhibitedPermissions { get; set; }

        private UserPowerCacheItem()
        {
            RoleIds = new List<string>();
            GrantedPermissions = new HashSet<string>();
            ProhibitedPermissions = new HashSet<string>();
        }

        public UserPowerCacheItem(string userId)
            : this()
        {
            UserId = userId;
        }
    }
}