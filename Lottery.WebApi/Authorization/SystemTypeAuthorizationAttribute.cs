using System;
using System.Collections.Generic;
using System.Web.Http.Filters;
using Lottery.Infrastructure.Collections;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Extensions;

namespace Lottery.WebApi.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class LotteryApiAuthenticationAttribute : AuthorizationFilterAttribute
    {
        private readonly IList<SystemType> _clientTypes;

        public LotteryApiAuthenticationAttribute(params string[] clientTypeStr)
        {
            _clientTypes = new List<SystemType>();
            foreach (var clientType in clientTypeStr)
            {
                _clientTypes.AddIfNotContains(clientType.ToEnum<SystemType>());
            }
        }

        public ICollection<SystemType> ClientType {
            get { return _clientTypes; }
        }
    }
}