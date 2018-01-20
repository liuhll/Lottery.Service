﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ECommon.Components;
using ECommon.Extensions;
using Effortless.Net.Encryption;
using Lottery.AppService.Power;
using Lottery.AppService.Role;
using Lottery.Core.Caching;
using Lottery.Dtos.Account;
using Lottery.Dtos.Power;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.Extensions;
using Lottery.QueryServices.UserInfos;

namespace Lottery.AppService.Account
{
    [Component]
    public class UserManager : IUserManager
    {
        private readonly IUserInfoService _userInfoService;
        private readonly IUserTicketService _userTicketService;
        private readonly IUserClientTypeQueryService _userClientTypeQueryService;
        private readonly IPowerManager _powerManager;
        private readonly IRoleManager _roleManager;
        private readonly ICacheManager _cacheManager;
        private readonly IUserPowerStore _userPowerStore;

        public UserManager(IUserInfoService userInfoService, 
            IUserTicketService userTicketService, 
            IUserClientTypeQueryService userClientTypeQueryService, 
            IPowerManager powerManager, 
            IRoleManager roleManager,
            ICacheManager cacheManager,
            IUserPowerStore userPowerStore)
        {
            _userInfoService = userInfoService;
            _userTicketService = userTicketService;
            _userClientTypeQueryService = userClientTypeQueryService;
            _powerManager = powerManager;
            _roleManager = roleManager;
            _cacheManager = cacheManager;
            _userPowerStore = userPowerStore;
        }

        public async Task<UserInfoViewModel> SignInAsync(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ValidationException("用户账号不允许为空");
            }
            if (!ValidUserAccount(userName))
            {
                throw new ValidationException($"账号{userName}不合法");
            }
            var userInfo = await _userInfoService.GetUserInfo(userName);

            if (userInfo == null)
            {
                throw new LotteryDataException($"不存在账号{userName}");
            }

            if (!userInfo.IsActive)
            {
                throw new LotteryDataException($"账号{userName}被冻结");
            }
            if (!VerifyPassword(userInfo,password))
            {
                throw new LotteryDataException($"密码错误");
            }
            return new UserInfoViewModel()
            {
               UserName = !string.IsNullOrEmpty(userInfo.UserName) ? userInfo.UserName : "",
               Email = !string.IsNullOrEmpty(userInfo.Email) ? userInfo.Email : "",
               Phone = !string.IsNullOrEmpty(userInfo.Phone) ? userInfo.Phone : "",
               IsActive = userInfo.IsActive,
               Id = userInfo.Id,
               AccountRegistType = userInfo.AccountRegistType,
            };


        }

        public Task<UserTicketDto> GetValidTicketInfo(string userId)
        {
            return _userTicketService.GetValidTicketInfo(userId);
        }

        public async Task<bool> IsExistAccount(string account)
        {
            var userInfo = await _userInfoService.GetUserInfo(account);
            if (userInfo == null)
            {
                return false;
            }
            return true;
        }

        public void VerifyUserSystemType(string userId, string systemType)
        {
            var userClientTypes = _userClientTypeQueryService.GetUserSystemTypes(userId);
            if (LotteryConstants.BackOfficeKey.Equals(systemType,StringComparison.CurrentCultureIgnoreCase))
            {
                if (!userClientTypes.Safe().Any(p => p.SystemType == SystemType.BackOffice && p.Status == 0))
                {
                    throw new LotteryAuthorizationException("该账号未被授权" + SystemType.BackOffice.GetChineseDescribe());
                }
            }
            else if (LotteryConstants.OfficialWebsite.Equals(systemType, StringComparison.CurrentCultureIgnoreCase))
            {
                // do nothing
            }
            else
            {
                if (userClientTypes.Safe().Any())
                {
                    if (!userClientTypes.Safe().Any(p => p.SystemType == SystemType.App && p.Status == 0))
                    {
                        throw new LotteryAuthorizationException("该账号未被授权访问" + SystemType.App.GetChineseDescribe());
                    }
                }
               
            }
        }

        public virtual async Task<bool> IsGrantedAsync(string userId, string powerCode)
        {
            try
            {
                return  await IsGrantedAsync(
                    userId,
                    _powerManager.GetPermission(powerCode)
                );
            }
            catch (ArgumentNullException e)
            {
                throw new LotteryAuthorizationException($"系统尚未设置${powerCode}权限码");
            }
        }

       
        public virtual async Task<bool> IsGrantedAsync(string userId, string urlPath, string method)
        {
            try
            {
                return await IsGrantedAsync(
                    userId,
                    _powerManager.GetPermission(urlPath,method)
                );
            }
            catch (ArgumentNullException e)
            {
                throw new LotteryAuthorizationException($"系统尚未对Api{urlPath}-{method}设置权限码");
            }
        }

        protected virtual async Task<bool> IsGrantedAsync(string userId, PowerDto power)
        {
            var isGranted = false;
            if (power == null)
            {
                throw new ArgumentNullException("系统尚未甚至该权限码");
            }
            //Get cached user permissions
            var cacheItem = GetUserPowerCacheItemAsync(userId);
            if (cacheItem == null)
            {
                throw new LotteryAuthorizationException($"没有权限{power.PowerName}");
            }
            //Check for user-specific value
            if (cacheItem.GrantedPermissions.Contains(power.PowerCode))
            {
                isGranted = true;
            }
            if (cacheItem.ProhibitedPermissions.Contains(power.PowerCode))
            {
                isGranted = false;
            }
            //Check for roles
            foreach (var roleId in cacheItem.RoleIds)
            {
                if (await _roleManager.IsGrantedAsync(roleId, power))
                {
                    isGranted = true;
                }
            }
            if (isGranted)
            {
                return true;
            }
            throw new LotteryAuthorizationException($"没有权限{power.PowerName}");
        }

        private UserPowerCacheItem GetUserPowerCacheItemAsync(string userId)
        {
            var redisKey = string.Format(RedisKeyConstants.USERINFO_POWER_KEY, userId);

            return _cacheManager.Get<UserPowerCacheItem>(redisKey, () =>
            {
                var userInfo = _userInfoService.GetUserInfoById(userId);
                if (userInfo == null)
                {
                    return null;
                }
                var newCacheItem = new UserPowerCacheItem(userId);
                var roles = _roleManager.GetUserRoles(userId);
            
                foreach (var role in roles.Safe())
                {
                    newCacheItem.RoleIds.Add(role.Id);
                }
                var userPowers = _userPowerStore.GetPermissions(userId).Safe();
                foreach (var permissionInfo in userPowers)
                {
                    if (permissionInfo.IsGranted)
                    {
                        newCacheItem.GrantedPermissions.Add(permissionInfo.PowerCode);
                    }
                    else
                    {
                        newCacheItem.ProhibitedPermissions.Add(permissionInfo.PowerCode);
                    }
                }
                return newCacheItem;
            });
        }

        private bool VerifyPassword(UserInfoDto userInfo, string inputPassword)
        {
            //var h1 = Hash.Create(HashType.MD5, inputPassword, userInfo.Id, true);
            var account = string.Empty;
            switch (userInfo.AccountRegistType)
            {
                case AccountRegistType.UserName:
                    account = userInfo.UserName;
                    break;
                case AccountRegistType.Email:
                    account = userInfo.Email;
                    break;
                case AccountRegistType.Phone:
                    account = userInfo.Phone;
                    break;

            }
            var h1 = Hash.Create(HashType.MD5, account + inputPassword, userInfo.AccountRegistType.ToString(), true);
            return h1.Equals(userInfo.Password);

        }

        private bool ValidUserAccount(string userName)
        {
            return Regex.IsMatch(userName, RegexConstants.UserName) ||
                   Regex.IsMatch(userName, RegexConstants.Email) ||
                   Regex.IsMatch(userName, RegexConstants.Phone);
        }
    }
}