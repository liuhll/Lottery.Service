using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Lottery.WebApi.RunTime.Security
{
    public static class LotteryClaimTypes
    {
 
        public static string UserName { get; set; } = ClaimTypes.Name;


        public static string UserId { get; set; } = ClaimTypes.NameIdentifier;
    }
}