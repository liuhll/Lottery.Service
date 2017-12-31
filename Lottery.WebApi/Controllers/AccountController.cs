using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ECommon.IO;
using ENode.Commanding;
using Lottery.AppService.Account;
using Lottery.Commands.UserInfos;
using Lottery.Dtos.Account;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.Tools;
using Lottery.WebApi.Extensions;
using Lottery.WebApi.RunTime.Security;
using Lottery.WebApi.ViewModels;

namespace Lottery.WebApi.Controllers
{
    [RoutePrefix("account")]
    public class AccountController : BaseApiController
    {
        private readonly IUserManager _userManager;

        public AccountController(IUserManager userManager,
            ICommandService commandService) : base(commandService)
        {
            _userManager = userManager;
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
            var userInfo = await _userManager.SignInAsync(loginModel.UserName, loginModel.Password);
            string token = CreateToken(userInfo);
            var ticketInfo = await _userManager.GetValidTiectInfo(userInfo.Id);

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
            var ticketInfo = await _userManager.GetValidTiectInfo(_lotterySession.UserId);
            await SendCommandAsync(new InvalidAccessTokenCommand(ticketInfo.Id));
            return "用户登出成功";
        }

        private string CreateToken(UserInfoViewModel userInfo)
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
            });

            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(LotteryConstants.JwtSecurityKey));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token =
                (JwtSecurityToken)
                tokenHandler.CreateJwtSecurityToken(issuer: Request.GetAudience(), audience: Request.GetIssuer(),
                    subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
