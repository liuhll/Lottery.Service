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

        public LotteryApiAuthenticationAttribute(string clientTypeStr)
        {
            var clientTypeStrArr = clientTypeStr.Split(',');
            _clientTypes = new List<SystemType>();
            foreach (var clientType in clientTypeStrArr)
            {
                _clientTypes.AddIfNotContains(clientType.ToEnum<SystemType>());
            }
        }

        public ICollection<SystemType> ClientType {
            get { return _clientTypes; }
        }
    }
}