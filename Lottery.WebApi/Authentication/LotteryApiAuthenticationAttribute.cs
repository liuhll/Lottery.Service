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
        private readonly IList<ClientType> _clientTypes;

        public LotteryApiAuthenticationAttribute(string clientTypeStr)
        {
            var clientTypeStrArr = clientTypeStr.Split(',');
            _clientTypes = new List<ClientType>();
            foreach (var clientType in clientTypeStrArr)
            {
                _clientTypes.AddIfNotContains(clientType.ToEnum<ClientType>());
            }
        }

        public ICollection<ClientType> ClientType {
            get { return _clientTypes; }
        }
    }
}