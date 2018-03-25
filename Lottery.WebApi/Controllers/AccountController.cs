using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using ECommon.IO;
using Effortless.Net.Encryption;
using ENode.Commanding;
using FluentValidation.Results;
using Lottery.AppService.Account;
using Lottery.AppService.IdentifyCode;
using Lottery.AppService.Validations;
using Lottery.Commands.IdentifyCodes;
using Lottery.Commands.LogonLog;
using Lottery.Commands.UserInfos;
using Lottery.Dtos.UserInfo;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Collections;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.Tools;
using Lottery.QueryServices.Canlogs;
using Lottery.QueryServices.Lotteries;
using Lottery.WebApi.Extensions;

namespace Lottery.WebApi.Controllers
{
    [RoutePrefix("account")]
    public class AccountController : BaseApiController
    {
        private readonly IUserManager _userManager;
        private readonly UserInfoInputValidator _userInfoInputValidator;
        private readonly UserProfileInputValidator _userProfileInputValidator;
        private readonly ILotteryQueryService _lotteryQueryService;
        private readonly IConLogQueryService _conLogQueryService;
        private readonly IIdentifyCodeAppService _identifyCodeAppService;

        public AccountController(IUserManager userManager,
            ICommandService commandService,
            UserInfoInputValidator userInfoInputValidator,
            UserProfileInputValidator userProfileInputValidator,
            ILotteryQueryService lotteryQueryService,
            IConLogQueryService conLogQueryService,
            IIdentifyCodeAppService identifyCodeAppService) : base(commandService)
        {
            _userManager = userManager;
            _userInfoInputValidator = userInfoInputValidator;
            _userProfileInputValidator = userProfileInputValidator;
            _lotteryQueryService = lotteryQueryService;
            _conLogQueryService = conLogQueryService;
            _identifyCodeAppService = identifyCodeAppService;
        }

        /// <summary>
        /// 用户登录接口
        /// </summary>
        /// <returns>返回Access Token</returns>
        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<string> Login(LoginViewModel loginModel)
        {
            string systemTypeId;
            if (!ValidateClient(loginModel.SystemType,out systemTypeId))
            {
                if (systemTypeId == LotteryConstants.BackOfficeKey)
                {
                    throw new LotteryAuthorizeException("不合法的客户端,请从合法途径登录");
                }
                throw new LotteryAuthorizeException("请先指定彩种");
            }
            var userInfo = await _userManager.SignInAsync(loginModel.UserName, loginModel.Password);
            // 验证该用户是否允许访问指定的客户端
            _userManager.VerifyUserSystemType(userInfo.Id, loginModel.SystemType);
            if (loginModel.IsForce)
            {
                await this.Logout(loginModel.IsForce,userInfo.Id, systemTypeId);
            }
            var clientNo = await _userManager.VerifyUserClientNo(userInfo.Id, systemTypeId);
            DateTime invalidDateTime;
            
            var token = _userManager.CreateToken(userInfo, systemTypeId,clientNo,out invalidDateTime);
            await SendCommandAsync(new AddConLogCommand(Guid.NewGuid().ToString(), userInfo.Id,clientNo,systemTypeId, Request.GetReuestIp(), invalidDateTime,userInfo.Id));

            return token;
        }

        /// <summary>
        /// 用户登出接口
        /// </summary>
        /// <returns></returns>
        [Route("logout")]
        [AllowAnonymous]
        public async Task<string> Logout(bool isForce = false, string userId = "", string systemTypeId = "")
        {
            if (!isForce)
            {
                if (string.IsNullOrEmpty(_lotterySession.UserId))
                {
                    throw new LotteryAuthorizationException("用户未登录，或已登出,无法调用该接口");
                }
                var conLog = _conLogQueryService.GetUserNewestConLog(_lotterySession.UserId,
                    _lotterySession.SystemTypeId, _lotterySession.ClientNo);
                if (conLog == null)
                {
                    throw new LotteryAuthorizationException("您已经登出,请不要重复操作");
                }
                await SendCommandAsync(new LogoutCommand(conLog.Id, _lotterySession.UserId));
            }
            else
            {
                var conLog = _conLogQueryService.GetUserNewestConLog(userId, systemTypeId, 1);
                if (conLog != null)
                {
                    await SendCommandAsync(new LogoutCommand(conLog.Id, userId));
                    Thread.Sleep(5000);
                }
            }

            return "用户登出成功";
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("register")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<string> CreateUser(UserInfoInput user)
        {
            ValidationResult validationResult = _userInfoInputValidator.Validate(user);
            if (!validationResult.IsValid)
            {
                throw new LotteryDataException(validationResult.Errors.Select(p => p.ErrorMessage).ToList().ToString(";"));
            }
  
            var validIdentifyCodeOutput = _identifyCodeAppService.ValidIdentifyCode(user.Account, user.IdentifyCode);

            if (validIdentifyCodeOutput.IsOvertime)
            {
                await SendCommandAsync(new InvalidIdentifyCodeCommand(validIdentifyCodeOutput.IdentifyCodeId,user.Account,_lotterySession.UserId));
                throw new LotteryDataException("验证码超时,请重新获取验证码");
            }
            if (!validIdentifyCodeOutput.IsValid)
            {
               // await SendCommandAsync(new InvalidIdentifyCodeCommand(validIdentifyCodeOutput.IdentifyCodeId, user.Account, _lotterySession.UserId));
                throw new LotteryDataException("您输入的验证码错误,请重新输入");
            }

            var accountRegType = AccountHelper.JudgeAccountRegType(user.Account);
            var isReg = await _userManager.IsExistAccount(user.Account);
            if (isReg)
            {
                await SendCommandAsync(new InvalidIdentifyCodeCommand(validIdentifyCodeOutput.IdentifyCodeId, user.Account, _lotterySession.UserId));
                throw new LotteryDataException("该账号已经存在");
            }

            // :todo 是否存在活动,以及查询获赠的积分
            var userInfoCommand = new AddUserInfoCommand(Guid.NewGuid().ToString(), user.Account, 
                EncryptPassword(user.Account, user.Password, accountRegType),
                user.ClientRegistType, accountRegType,0);

            var commandResult = await SendCommandAsync(userInfoCommand);
            if (commandResult.Status != AsyncTaskStatus.Success)
            {
                throw new LotteryDataException("创建用户失败");
            }
            await SendCommandAsync(new InvalidIdentifyCodeCommand(validIdentifyCodeOutput.IdentifyCodeId, user.Account,
                user.Account));
            return "注册用户成功";
        }

        /// <summary>
        /// 绑定用户邮箱或手机号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("userprofie")]
        [HttpPut]
        public async Task<string> BindUserProfile(UserProfileInput input)
        {
            ValidationResult validationResult = _userProfileInputValidator.Validate(input);
            if (!validationResult.IsValid)
            {
                throw new LotteryDataException(validationResult.Errors.Select(p => p.ErrorMessage).ToList().ToString(";"));
            }

            // :todo 手机号码验证码 | 电子邮箱验证码
            var isReg = await _userManager.IsExistAccount(input.Profile);
            if (isReg)
            {
                throw new LotteryDataException("已经存在该账号,不允许被绑定");
            }
            //var userInfo = await _userInfoService.GetUserInfoById(_lotterySession.UserId);
            AsyncTaskResult result = null;
            if (input.ProfileType == AccountRegistType.Email)
            {
                var bindUserEmailCommand = new BindUserEmailCommand(_lotterySession.UserId, input.Profile);
                result = await SendCommandAsync(bindUserEmailCommand);
            }
            else if (input.ProfileType == AccountRegistType.Phone)
            {

                var bindUserPhoneCommand = new BindUserPhoneCommand(_lotterySession.UserId, input.Profile);
                result = await SendCommandAsync(bindUserPhoneCommand);
            }

            Debug.Assert(result != null, "result != null");
            if (result.Status == AsyncTaskStatus.Success)
            {
                return "用户信息绑定成功";
            }
            throw new LotteryDataException("绑定失败");
        }

        #region 私有方法

        private string EncryptPassword(string userAccount, string userPassword, AccountRegistType accountRegType)
        {
            var pwd = Hash.Create(HashType.MD5, userAccount + userPassword, accountRegType.ToString(), true);
            return pwd;
        }

       

        private bool ValidateClient(string clientType, out string clientTypeId)
        {
            if (LotteryConstants.BackOfficeKey.Equals(clientType, StringComparison.CurrentCultureIgnoreCase))
            {
                clientTypeId = LotteryConstants.BackOfficeKey;
                return true;
            }
            if (LotteryConstants.OfficialWebsite.Equals(clientType, StringComparison.CurrentCultureIgnoreCase))
            {
                clientTypeId = LotteryConstants.OfficialWebsite;
                return true;
            }
            var result = _lotteryQueryService.GetLotteryInfoByCode(clientType);
            if (result == null)
            {
                throw new LotteryDataException($"不存在编码为{clientType}的彩种");
            }
            clientTypeId = result.Id;
            return true;
        }

        #endregion
    }
}
