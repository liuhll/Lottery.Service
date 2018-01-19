using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using ECommon.Components;
using ECommon.Extensions;
using Effortless.Net.Encryption;
using Lottery.Dtos.Account;
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

        public UserManager(IUserInfoService userInfoService, 
            IUserTicketService userTicketService, 
            IUserClientTypeQueryService userClientTypeQueryService)
        {
            _userInfoService = userInfoService;
            _userTicketService = userTicketService;
            _userClientTypeQueryService = userClientTypeQueryService;
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