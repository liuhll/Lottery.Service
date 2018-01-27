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
using Lottery.AppService.Account;
using Lottery.Commands.LogonLog;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.Extensions;
using Lottery.Infrastructure.RunTime.Session;
using Lottery.QueryServices.Canlogs;
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
        private readonly IUserManager _userManager;
        private readonly IConLogQueryService _conLogQueryService;

        private static string[] whitelist = new string[] { "/account/login" };

        public TokenValidationHandler()
        {
            _commandService = ObjectContainer.Resolve<ICommandService>();
            _lotterySession = NullLotterySession.Instance;
            _userManager = ObjectContainer.Resolve<IUserManager>();
            _conLogQueryService = ObjectContainer.Resolve<IConLogQueryService>();
        }

        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;

            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                if (whitelist.Any(p=>p == request.RequestUri.AbsolutePath.ToLower()) || request.RequestUri.AbsolutePath.ToLower().Contains("swagger"))
                {
                    return false;
                }
                return true;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            string token;
            int errorCode;
            string errorMessage;

            //determine whether a jwt exists or not
            if (!TryRetrieveToken(request, out token))
            {               
                return await base.SendAsync(request, cancellationToken);
            }

            try
            {
                var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(LotteryConstants.JwtSecurityKey));

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

                var principal = handler.ValidateToken(token, validationParameters, out securityToken);
                Thread.CurrentPrincipal = principal;
                HttpContext.Current.User = principal;

                var conLog = _conLogQueryService.GetUserConLog(_lotterySession.UserId, _lotterySession.SystemTypeId, _lotterySession.ClientNo,
                    securityToken.ValidTo.ToLocalTime());
                if (conLog.LogoutTime.HasValue)
                {
                    throw new LotteryAuthorizationException("您已经登出,请重新登录");
                }
                var okResponse = await base.SendAsync(request, cancellationToken);
                //extract and assign the user of the jwt           
                if ((securityToken.ValidTo - DateTime.UtcNow).TotalMinutes <= 3)
                {
                    DateTime invalidTime;                 
                    var refreshToken =
                        _userManager.UpdateToken(_lotterySession.UserId, _lotterySession.SystemTypeId,
                            _lotterySession.ClientNo, out invalidTime);
                    okResponse.Headers.Add("access-token", refreshToken);

                    await _commandService.ExecuteAsync(
                        new UpdateTokenCommand(conLog.Id, invalidTime, _lotterySession.UserId));
                }
                return okResponse;
            }
            catch (SecurityTokenInvalidLifetimeException ex)
            {
                var tokenInfo = ex.GetTokenInfo();
                var conLog = _conLogQueryService.GetUserConLog(tokenInfo.NameId,tokenInfo.SystemTypeId, tokenInfo.ClientNo,
                    tokenInfo.Exp);
                if (conLog != null)
                {
                    await _commandService.ExecuteAsync(new LogoutCommand(conLog.Id, tokenInfo.NameId));
                }

                errorCode = ErrorCode.OvertimeToken;
                errorMessage = "登录超时,请重新超时";
            }
            catch (SecurityTokenValidationException ex)
            {
                errorCode = ErrorCode.InvalidToken;
                errorMessage = "无效的Token" + ex.Message;
            }
            catch (LotteryAuthorizationException ex)
            {
                errorCode = ErrorCode.AuthorizationFailed;
                errorMessage = ex.Message;
            }
            catch (NullReferenceException)
            {
                errorCode = ErrorCode.InvalidToken;
                errorMessage = "无效的Token,原因:该Token已失效";
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.InvalidToken;
                errorMessage = "无效的Token,原因:" + ex.Message;
            }

            var errorResponse = request.CreateResponse(HttpStatusCode.OK,new ResponseMessage(new ErrorInfo(errorCode, errorMessage),true));
            return await Task<HttpResponseMessage>.Factory.StartNew(() => errorResponse);
        }

        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires)
                    return true;
            }         
            return false;
        }
    }
}