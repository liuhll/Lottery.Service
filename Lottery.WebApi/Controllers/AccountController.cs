using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using ECommon.IO;
using Effortless.Net.Encryption;
using ENode.Commanding;
using FluentValidation.Results;
using Lottery.AppService.Account;
using Lottery.AppService.Operations;
using Lottery.Commands.UserInfos;
using Lottery.Core.Domain.LotteryInfos;
using Lottery.Dtos.Account;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Collections;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.Tools;
using Lottery.QueryServices.Lotteries;
using Lottery.QueryServices.UserInfos;
using Lottery.WebApi.Extensions;
using Lottery.WebApi.RunTime.Security;
using Lottery.WebApi.Validations;
using Lottery.WebApi.ViewModels;
using Microsoft.IdentityModel.Tokens;

namespace Lottery.WebApi.Controllers
{
    [RoutePrefix("account")]
    public class AccountController : BaseApiController
    {
        private readonly IUserManager _userManager;
        private readonly UserInfoInputValidator _userInfoInputValidator;
        private readonly UserProfileInputValidator _userProfileInputValidator;
        private readonly IMemberAppService _memberAppService;
        private readonly ILotteryQueryService _lotteryQueryService;


        public AccountController(IUserManager userManager,
            ICommandService commandService,
            UserInfoInputValidator userInfoInputValidator,
            UserProfileInputValidator userProfileInputValidator,
            IMemberAppService memberAppService, 
            ILotteryQueryService lotteryQueryService) : base(commandService)
        {
            _userManager = userManager;
            _userInfoInputValidator = userInfoInputValidator;
            _userProfileInputValidator = userProfileInputValidator;
            _memberAppService = memberAppService;
            _lotteryQueryService = lotteryQueryService;
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
            string clientTypeId;
            if (!ValidateClient(loginModel.ClientType,out clientTypeId))
            {
                if (clientTypeId == LotteryConstants.BackOfficeKey)
                {
                    throw new LotteryAuthorizationException("不合法的客户端,请从合法途径登录");
                }
                throw new LotteryAuthorizationException("请先指定彩种");
            }
            var userInfo = await _userManager.SignInAsync(loginModel.UserName, loginModel.Password);
            string token = CreateToken(userInfo, clientTypeId);
            var ticketInfo = await _userManager.GetValidTicketInfo(userInfo.Id);

            if (ticketInfo == null)
            {
                await SendCommandAsync(new AddAccessTokenCommand(Guid.NewGuid().ToString(), userInfo.Id, token,
                        userInfo.Id));
            }
            else
            {
                await SendCommandAsync(
                        new UpdateAccessTokenCommand(ticketInfo.Id, userInfo.Id, token, userInfo.Id));

            }
            return token;
        }

        private bool ValidateClient(string clientType,out string clientTypeId)
        {
            if (LotteryConstants.BackOfficeKey.Equals(clientType,StringComparison.CurrentCultureIgnoreCase))
            {
                clientTypeId = LotteryConstants.BackOfficeKey;
                return true;
            }
            if (LotteryConstants.OfficialWebsite.Equals(clientType,StringComparison.CurrentCultureIgnoreCase))
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

        /// <summary>
        /// 用户登出接口
        /// </summary>
        /// <returns></returns>
        [Route("logout")]
        public async Task<string> Logout()
        {
            if (string.IsNullOrEmpty(_lotterySession.UserId))
            {
                throw new LotteryAuthorizationException("用户未登录，或已登出,无法调用该接口");
            }
            var ticketInfo = await _userManager.GetValidTicketInfo(_lotterySession.UserId);
            await SendCommandAsync(new InvalidAccessTokenCommand(ticketInfo.Id));
            return "用户登出成功";
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("userinfo")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<string> CreateUser(UserInfoInput user)
        {
            ValidationResult validationResult = _userInfoInputValidator.Validate(user);
            if (!validationResult.IsValid)
            {
                throw new LotteryDataException(validationResult.Errors.Select(p => p.ErrorMessage).ToList().ToString(";"));
            }
            var isReg = await _userManager.IsExistAccount(user.Account);
            if (isReg)
            {
                throw new LotteryDataException("该账号已经存在");
            }
            var accountRegType = ReferAccountRegType(user.Account);
            var userInfoCommand = new AddUserInfoCommand(Guid.NewGuid().ToString(), user.Account, EncryptPassword(user.Account, user.Password, accountRegType),
                user.ClientRegistType, accountRegType);

            var commandResult = await SendCommandAsync(userInfoCommand);
            if (commandResult.Status != AsyncTaskStatus.Success)
            {
                throw new LotteryDataException("创建用户失败");
            }
            return "创建用户成功";
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

        private AccountRegistType ReferAccountRegType(string userAccount)
        {
            if (Regex.IsMatch(userAccount, RegexConstants.UserName))
            {
                return AccountRegistType.UserName;
            }
            if (Regex.IsMatch(userAccount, RegexConstants.Email))
            {
                return AccountRegistType.Email;
            }
            if (Regex.IsMatch(userAccount, RegexConstants.Phone))
            {
                return AccountRegistType.Phone;
            }
            throw new LotteryDataException("注册账号不合法");

        }

        private string CreateToken(UserInfoViewModel userInfo,string clientTypeId)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddDays(ConfigHelper.ValueInt("passwordExpire"));

            // http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(LotteryClaimTypes.UserName, userInfo.UserName),
                new Claim(LotteryClaimTypes.UserId,userInfo.Id),
                new Claim(LotteryClaimTypes.Email,userInfo.Email),
                new Claim(LotteryClaimTypes.Phone,userInfo.Phone),
                new Claim(LotteryClaimTypes.ClientType,clientTypeId), 
                new Claim(LotteryClaimTypes.MemberRank,_memberAppService.ConcludeUserMemRank(userInfo.Id,clientTypeId)), 
            });

            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(LotteryConstants.JwtSecurityKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token =
                (JwtSecurityToken)
                tokenHandler.CreateJwtSecurityToken(issuer: Request.GetAudience(), audience: Request.GetIssuer(),
                    subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        #endregion
    }
}
