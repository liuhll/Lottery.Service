using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using Lottery.Infrastructure;
using Lottery.WebApi.Extensions;
using Lottery.WebApi.ViewModels;

namespace Lottery.WebApi.Controllers
{
    [RoutePrefix("account")]
    public class AccountController : BaseApiController
    {
        /// <summary>
        /// 用户登录接口
        /// </summary>
        /// <returns>返回Access Token</returns>
        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public string Login(LoginViewModel login)
        {
            //var loginResponse = new LoginResponse { };
           // LoginRequest loginrequest = new LoginRequest { };
            //loginrequest.Username = login.UserName.ToLower();
            //loginrequest.Password = login.Password;

            IHttpActionResult response;
            HttpResponseMessage responseMsg = new HttpResponseMessage();
            bool isUsernamePasswordValid = false;

            if (login != null)
                isUsernamePasswordValid = login.Password == "admin" ? true : false;
            // if credentials are valid
            if (isUsernamePasswordValid)
            {
                string token = createToken(login.UserName);
                //return the token
                return token;
            }
            else
            {
                throw new HttpRequestValidationException("错误");
            }
        }

        private string createToken(string username)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddDays(7);

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            });

            var now = DateTime.UtcNow;
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
