using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using ECommon.Components;
using ENode.Commanding;
using Lottery.Commands.UserInfos;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.RunTime.Session;
using Lottery.QueryServices.UserInfos;
using Lottery.WebApi.Extensions;
using Lottery.WebApi.Result.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.IdentityModel.Tokens;
using Thinktecture.IdentityModel.Extensions;
using SecurityToken = Microsoft.IdentityModel.Tokens.SecurityToken;
using SecurityTokenValidationException = Microsoft.IdentityModel.Tokens.SecurityTokenValidationException;

namespace Lottery.WebApi.Authentication
{
    internal class TokenValidationHandler : DelegatingHandler
    {
        private readonly ICommandService _commandService;
        private readonly ILotterySession _lotterySession;
        private readonly IUserTicketService _userTicketService;

        public TokenValidationHandler()
        {
            _commandService = ObjectContainer.Resolve<ICommandService>();
            _userTicketService = ObjectContainer.Resolve<IUserTicketService>();
            _lotterySession = NullLotterySession.Instance;
        }


        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string token;

            string errorMessage;
            //determine whether a jwt exists or not
            if (!TryRetrieveToken(request, out token))
            {
                //allow requests with no token - whether a action method needs an authentication can be set with the claimsauthorization attribute
                return await base.SendAsync(request, cancellationToken);
            }

            try
            {            
                var securityKey =
                    new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                        System.Text.Encoding.Default.GetBytes(LotteryConstants.JwtSecurityKey));

                SecurityToken securityToken;
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = request.GetAudience(),
                    ValidIssuer = request.GetIssuer(),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = this.LifetimeValidator,
                    IssuerSigningKey = securityKey,
                };
                //extract and assign the user of the jwt
                Thread.CurrentPrincipal = handler.ValidateToken(token, validationParameters, out securityToken);
                HttpContext.Current.User = handler.ValidateToken(token, validationParameters, out securityToken);

                // 再次验证token的合法性
                var userTicket = await _userTicketService.GetValidTicketInfo(_lotterySession.UserId);
                if (userTicket == null)
                {
                    throw new LotteryAuthorizationException("用户未登录,请重新登录");
                }
                if (string.IsNullOrEmpty(userTicket.AccessToken))
                {
                    throw new LotteryAuthorizationException("用户已登出,请重新登录");
                }
                if (!token.Equals(userTicket.AccessToken))
                {
                    throw new LotteryAuthorizationException("无效的token,可能用户已经从其他终端登录");
                }

                return await base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException ex)
            {

                statusCode = HttpStatusCode.Unauthorized;
                errorMessage = ex.Message;
            }
            catch (LotteryAuthorizationException ex)
            {
                statusCode = HttpStatusCode.Unauthorized;
                errorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                statusCode = HttpStatusCode.Unauthorized;
                errorMessage = "无效的Token";
            }

            var response = request.CreateResponse(statusCode,new ResponseMessage(new ErrorInfo(errorMessage),true));

            return await Task<HttpResponseMessage>.Factory.StartNew(() => response);
        }

        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires)
                    return true;
            }
            var jwtsecurityToken = (JwtSecurityToken) securityToken;
            var userId = jwtsecurityToken.Payload["nameid"].ToString();
            if (!string.IsNullOrEmpty(userId))
            {
                var userTicket = _userTicketService.GetValidTicketInfo(userId).Result;
                _commandService.Send(new InvalidAccessTokenCommand(userTicket.Id));
            }
            return false;
        }
    }
}