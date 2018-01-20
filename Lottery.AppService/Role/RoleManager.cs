using System.Collections.Generic;
using System.Threading.Tasks;
using ECommon.Components;
using Lottery.AppService.Power;
using Lottery.Core.Caching;
using Lottery.Dtos.Power;
using Lottery.Dtos.RoleDto;
using Lottery.Infrastructure;
using Lottery.QueryServices.Roles;

namespace Lottery.AppService.Role
{
    [Component]
    public class RoleManager : IRoleManager
    {
        private readonly IRoleQueryService _roleQueryService;
        private readonly IPowerManager _powerManager;
        private readonly ICacheManager _cacheManager;
        private readonly IRolePowerStore _rolePowerStore;


        public RoleManager(IRoleQueryService roleQueryService,
            IPowerManager powerManager,
            ICacheManager cacheManager,
            IRolePowerStore rolePowerStore)
        {
            _roleQueryService = roleQueryService;
            _powerManager = powerManager;
            _cacheManager = cacheManager;
            _rolePowerStore = rolePowerStore;
        }

        public async Task<bool> IsGrantedAsync(string roleId, string powerCode)
        {
            return await IsGrantedAsync(roleId, _powerManager.GetPermission(powerCode));
        }

        public async Task<bool> IsGrantedAsync(string roleId, PowerDto power)
        {
            //Get cached role permissions
            var cacheItem = await GetRolePermissionCacheItemAsync(roleId);

            //Check the permission
            return cacheItem.GrantedPowers.Contains(power.PowerCode);
        }

        private Task<RolePowerCacheItem> GetRolePermissionCacheItemAsync(string roleId)
        {
            var redisKey = string.Format(RedisKeyConstants.ROLE_POWER_KEY, roleId);
            return Task.FromResult(_cacheManager.Get<RolePowerCacheItem>(redisKey, () =>
            {
                var newCacheItem = new RolePowerCacheItem(roleId);
                foreach (var powerInfo in _rolePowerStore.GetPermissions(roleId))
                {
                    if (powerInfo.IsGranted)
                    {
                        newCacheItem.GrantedPowers.Add(powerInfo.PowerCode);
                    }                   
                }
                return newCacheItem;
            }));
        }

        public ICollection<RoleDto> GetUserRoles(string userId)
        {
            return _roleQueryService.GetUserRoles(userId);
        }
    }
}