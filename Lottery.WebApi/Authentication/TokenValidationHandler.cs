using ECommon.Components;
using ECommon.Logging;
using ENode.Commanding;
using Lottery.AppService.Account;
using Lottery.Commands.LogonLog;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.Logs;
using Lottery.Infrastructure.RunTime.Session;
using Lottery.QueryServices.Canlogs;
using Lottery.WebApi.Extensions;
using Lottery.WebApi.Result.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using SecurityToken = Microsoft.IdentityModel.Tokens.SecurityToken;
using SecurityTokenInvalidLifetimeException = Microsoft.IdentityModel.Tokens.SecurityTokenInvalidLifetimeException;
using SecurityTokenValidationException = Microsoft.IdentityModel.Tokens.SecurityTokenValidationException;

namespace Lottery.WebApi.Authentication
{
    internal class TokenValidationHandler : DelegatingHandler
    {
        private readonly ICommandService _commandService;
        private readonly ILotterySession _lotterySession;
        private readonly IUserManager _userManager;
        private readonly IConLogQueryService _conLogQueryService;
        private static ILogger _logger;

        private static Tuple<string, string>[] whitelist = new Tuple<string, string>[] { new Tuple<string, string>("/account/login","POST"),
            new Tuple<string, string>("/account/register","POST"), new Tuple<string, string>("/v1/lottery/list","GET"),
            new Tuple<string, string>("/v1/message/identifycode1","GET"), new Tuple<string, string>("/v1/message/identifycode","POST"),
            new Tuple<string, string>("/account/retrievepassword","PUT"), new Tuple<string, string>("/v1/sell/notify","POST"),
            new Tuple<string, string>("/v1/operation/wechatconfig","GET"),
        };

        public TokenValidationHandler()
        {
            _commandService = ObjectContainer.Resolve<ICommandService>();
            _lotterySession = NullLotterySession.Instance;
            _userManager = ObjectContainer.Resolve<IUserManager>();
            _conLogQueryService = ObjectContainer.Resolve<IConLogQueryService>();
            _logger = NullLotteryLogger.Instance;
        }

        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;

            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                if (whitelist.Any(p => request.RequestUri.AbsolutePath.ToLower().Contains(p.Item1)
                && request.Method.Method.ToUpper().Equals(p.Item2))
                || request.RequestUri.AbsolutePath.ToLower().Contains("swagger"))
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

            if ("OPTIONS".Equals(request.Method.Method.ToUpper()))
            {
                return await base.SendAsync(request, cancellationToken);
            }

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
                    ValidAudience = request.GetAudience(),  // LotteryConstants.ValidAudience,
                    ValidIssuer = request.GetIssuer(),  // LotteryConstants.ValidIssuer,
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
                try
                {
                    var tokenInfo = ex.GetTokenInfo();
                    var conLog = _conLogQueryService.GetUserConLog(tokenInfo.NameId, tokenInfo.SystemTypeId, tokenInfo.ClientNo,
                        tokenInfo.Exp);
                    if (conLog != null)
                    {
                        await _commandService.ExecuteAsync(new LogoutCommand(conLog.Id, tokenInfo.NameId));
                    }
                    errorCode = ErrorCode.OvertimeToken;
                    errorMessage = "登录超时,请重新登录";
                }
                catch (Exception e)
                {
                    errorCode = ErrorCode.InvalidToken;
                    errorMessage = e.Message;
                }
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

            var errorResponse = request.CreateResponse(HttpStatusCode.OK, new ResponseMessage(new ErrorInfo(errorCode, errorMessage), true));
            // 对无效token，错误的请求解决无法跨域的问题
            errorResponse.Headers.Add("Access-Control-Allow-Origin", "*");
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