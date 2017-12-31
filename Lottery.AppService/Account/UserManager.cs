using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using ECommon.Components;
using Effortless.Net.Encryption;
using Lottery.Dtos.Account;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Exceptions;
using Lottery.QueryServices.UserInfos;

namespace Lottery.AppService.Account
{
    [Component]
    public class UserManager : IUserManager
    {
        private readonly IUserInfoService _userInfoService;
        private readonly IUserTicketService _userTicketService;


        public UserManager(IUserInfoService userInfoService, 
            IUserTicketService userTicketService)
        {
            _userInfoService = userInfoService;
            _userTicketService = userTicketService;
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
               UserRegistType = userInfo.UserRegistType,
            };


        }

        public Task<UserTicketDto> GetValidTiectInfo(string userId)
        {
            return _userTicketService.GetValidTicketInfo(userId);
        }

        private bool VerifyPassword(UserInfoDto userInfo, string inputPassword)
        {
            var h1 = Hash.Create(HashType.MD5, inputPassword, userInfo.Id, true);
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