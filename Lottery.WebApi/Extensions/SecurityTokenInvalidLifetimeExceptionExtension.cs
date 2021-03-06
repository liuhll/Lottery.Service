﻿using Lottery.Infrastructure;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.Extensions;
using Lottery.Infrastructure.RunTime.Security;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;

namespace Lottery.WebApi.Extensions
{
    public static class SecurityTokenInvalidLifetimeExceptionExtension
    {
        public static TokenInfo GetTokenInfo(this SecurityTokenInvalidLifetimeException exception)
        {
            try
            {
                var errorMsg = exception.Message;

                var planloadStr = errorMsg.Substring(errorMsg.IndexOfCount("{", 2) - 1);
                planloadStr = planloadStr.Remove(planloadStr.Length - 2);

                var jObj = JObject.Parse(planloadStr);

                return new TokenInfo()
                {
                    NameId = jObj["nameid"].ToString(),
                    Iat = DateTimeExtensions.TimeStampConvetDateTime(Convert.ToInt64(jObj["iat"].ToString())),
                    Exp = DateTimeExtensions.TimeStampConvetDateTime(Convert.ToInt64(jObj["exp"].ToString())),
                    Memberrank = jObj[LotteryClaimTypes.MemberRank].ToString(),
                    ClientNo = Convert.ToInt32(jObj[LotteryClaimTypes.ClientNo].ToString()),
                    SystemTypeId = jObj[LotteryClaimTypes.SystemType].ToString(),
                };
            }
            catch (Exception e)
            {
                throw new LotteryAuthorizationException("无效的Token", ErrorCode.InvalidToken);
            }
        }
    }

    public class TokenInfo
    {
        public string NameId { get; set; }

        public DateTime Iat { get; set; }

        public DateTime Exp { get; set; }

        public string Memberrank { get; set; }

        public int ClientNo { get; set; }

        public string SystemTypeId { get; set; }
    }
}