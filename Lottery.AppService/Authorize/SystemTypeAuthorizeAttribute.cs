using Lottery.Infrastructure.Collections;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.AppService.Authorize
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class SystemTypeAuthorizeAttribute : Attribute, ISystemTypeAuthorizeAttribute
    {
        private readonly IList<SystemType> _clientTypes;

        public SystemTypeAuthorizeAttribute(params string[] clientTypeStr)
        {
            _clientTypes = new List<SystemType>();
            foreach (var clientType in clientTypeStr)
            {
                _clientTypes.AddIfNotContains(clientType.ToEnum<SystemType>());
            }
        }

        public SystemType[] ClientTypes
        {
            get { return _clientTypes.ToArray(); }
        }
    }
}