using System.Collections.Generic;

namespace Lottery.Dtos.Power
{
    public class RolePowerCacheItem
    {
        public const string CacheStoreName = "LotteryRolePowers";

        public string RoleId { get; set; }

        public HashSet<string> GrantedPowers { get; set; }

        public RolePowerCacheItem()
        {
            GrantedPowers = new HashSet<string>();
        }

        public RolePowerCacheItem(string roleId)
            : this()
        {
            RoleId = roleId;
        }
    }
}