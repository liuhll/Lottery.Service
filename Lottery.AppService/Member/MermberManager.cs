using System;
using System.Net.Http;
using System.Threading.Tasks;
using ECommon.Components;
using ECommon.Extensions;
using Lottery.AppService.Account;
using Lottery.AppService.Operations;
using Lottery.AppService.Power;
using Lottery.AppService.Role;
using Lottery.Core.Caching;
using Lottery.Dtos.Menbers;
using Lottery.Dtos.Power;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;
using Lottery.QueryServices.Operations;
using Lottery.QueryServices.UserInfos;

namespace Lottery.AppService.Member
{
    [Component]
    public class MermberManager : UserManager, IMermberManager
    {
        private readonly IMemberPowerStore _memberPowerStore;
        private readonly IMemberQueryService _memberQueryService;

        public MermberManager(IUserInfoService userInfoService,
            IUserTicketService userTicketService, 
            IUserClientTypeQueryService userClientTypeQueryService,
            IPowerManager powerManager, 
            IRoleManager roleManager,
            ICacheManager cacheManager,
            IUserPowerStore userPowerStore, 
            IMemberPowerStore memberPowerStore,
            IMemberQueryService memberQueryService,
            IMemberAppService memberAppService) :
            base(userInfoService,
                userTicketService,
                userClientTypeQueryService,
                powerManager,
                roleManager,
                cacheManager, 
                userPowerStore,
                memberAppService)
        {
            _memberPowerStore = memberPowerStore;
            _memberQueryService = memberQueryService;
        }
        async Task<bool> IMermberManager.IsGrantedAsync(string userId, string lotteryId, string powerCode)
        {
            try
            {
                return await IsGrantedAsync(
                    userId,
                    _powerManager.GetPermission(powerCode)
                );
            }
            catch (ArgumentNullException e)
            {
                throw new LotteryAuthorizeException($"系统尚未设置{powerCode}权限码");
            }
        }

        public async Task<bool> IsGrantedAsync(string userId, string lotteryId, string urlPath, HttpMethod method)
        {
            try
            {
                return await IsGrantedAsync(
                    userId,
                    lotteryId,
                    _powerManager.GetPermission(urlPath,method.ToString())
                );
            }
            catch (ArgumentNullException e)
            {
                throw new LotteryAuthorizeException($"系统尚未对Api: {urlPath}--{method} 设置权限码");
            }
        }

        public MemberRank GetUserMemberRank(string userId, string lotteryId)
        {
            var memberInfo = _memberQueryService.GetUserMenberInfo(userId, lotteryId);
            if (memberInfo == null)
            {
                return MemberRank.Ordinary;
            }
            return memberInfo.ComputeMemberRank();

        }

        protected virtual async Task<bool> IsGrantedAsync(string userId,string lotteryId, PowerDto power)
        {
            var isGranted = false;
            if (power == null)
            {
                throw new ArgumentNullException("系统不存在该权限码");
            }
            var memberRank = GetUserMemberRank(userId, lotteryId);
            //Get cached user permissions
            var cacheItem = await GetMemberPowerCacheItemAsync(lotteryId,memberRank);
            if (cacheItem == null)
            {
                throw new LotteryAuthorizeException($"没有权限{power.PowerName}");
            }
            //Check for user-specific value
            if (cacheItem.GrantedPowers.Contains(power.PowerCode))
            {
                isGranted = true;
            }
            //Check for roles
            foreach (var roleId in cacheItem.RoleIds)
            {
                if (await _roleManager.IsGrantedMermberAsync(roleId, power))
                {
                    isGranted = true;
                }
            }
            if (isGranted)
            {
                return true;
            }
            throw new LotteryAuthorizeException($"没有{power.PowerName}权限");
        }

        protected virtual Task<MemberRankPowerCacheItem> GetMemberPowerCacheItemAsync(string lotteryId, MemberRank memberRank)
        {
            var redisKey = string.Format(RedisKeyConstants.MEMBERRANK_POWER_KEY,lotteryId, memberRank);

            return Task.FromResult(_cacheManager.Get<MemberRankPowerCacheItem>(redisKey, () =>
            {

                var newCacheItem = new MemberRankPowerCacheItem(lotteryId, memberRank);
                var roles = _roleManager.GetMermberRoles(lotteryId,memberRank);

                foreach (var role in roles.Safe())
                {
                    newCacheItem.RoleIds.Add(role.Id);
                }
                var memberPowers = _memberPowerStore.GetMermberPermissions(lotteryId, (int)memberRank).Safe();
                foreach (var permissionInfo in memberPowers)
                {
                    if (permissionInfo.IsGranted)
                    {
                        newCacheItem.GrantedPowers.Add(permissionInfo.PowerCode);
                    }

                }
                return newCacheItem;
            }));
        }
    }
}