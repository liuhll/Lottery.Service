﻿using System.Security.Claims;
using System.Web;

namespace Lottery.WebApi.RunTime.Session
{
    public class WebApiPrincipalAccessor : DefaultPrincipalAccessor
    {
        public override ClaimsPrincipal Principal => HttpContext.Current.User as ClaimsPrincipal ?? base.Principal;
    
    }
}