using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using ECommon.Components;
using ENode.Commanding;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.RunTime.Session;
using Lottery.QueryServices.UserInfos;
using Lottery.WebApi.Extensions;
using Lottery.WebApi.Result.Models;
using Microsoft.IdentityModel.Tokens;
using SecurityToken = Microsoft.IdentityModel.Tokens.SecurityToken;
using SecurityTokenValidationException = Microsoft.IdentityModel.Tokens.SecurityTokenValidationException;
using SecurityTokenInvalidLifetimeException = Microsoft.IdentityModel.Tokens.SecurityTokenInvalidLifetimeException;

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
                var principal = handler.ValidateToken(token, validationParameters, out securityToken);
                Thread.CurrentPrincipal = principal;
                HttpContext.Current.User = principal;

                return await base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenInvalidLifetimeException ex)
            {
                // Update Token          
                statusCode = HttpStatusCode.Unauthorized;
                errorMessage = "Token登录超时";
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
               // _commandService.Send(new InvalidAccessTokenCommand(userTicket.Id));
            }
            return false;
        }
    }
}