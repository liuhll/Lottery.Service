using System;
using Lottery.Infrastructure.Extensions;
using Lottery.Infrastructure.Json;
using Lottery.Infrastructure.RunTime.Security;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace Lottery.WebApi.Extensions
{
    public static class SecurityTokenInvalidLifetimeExceptionExtension
    {
        public static TokenInfo GetTokenInfo(this SecurityTokenInvalidLifetimeException exception)
        {
            var errorMsg = exception.Message;

            var planloadStr = errorMsg.Substring(errorMsg.IndexOfCount("{", 2) -1);
            planloadStr = planloadStr.Remove(planloadStr.Length - 2);

            var jObj = JObject.Parse(planloadStr);

            return new TokenInfo()
            {
                NameId = jObj["nameid"].ToString(),
                Iat = DateTimeExtensions.TimeStampConvetDateTime(Convert.ToInt64(jObj["iat"].ToString())),
                Exp = DateTimeExtensions.TimeStampConvetDateTime(Convert.ToInt64(jObj["exp"].ToString())),
                Memberrank = jObj[LotteryClaimTypes.MemberRank].ToString()
            };
        }
    }

    public class TokenInfo
    {
        public string NameId { get; set; }

        public DateTime Iat { get; set; }

        public DateTime Exp { get; set; }

        public string Memberrank { get; set; }

    }
}