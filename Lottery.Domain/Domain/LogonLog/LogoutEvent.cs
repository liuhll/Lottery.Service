using System;
using ENode.Eventing;

namespace Lottery.Core.Domain.LogonLog
{
    public class LogoutEvent : DomainEvent<string>
    {
        private LogoutEvent()
        {
           
        }

        public LogoutEvent(string userId,DateTime loginTime)
        {
            UserId = userId;
            LoginTime = loginTime;
        }


        public string UserId { get; private set; }

        public DateTime LoginTime { get; private set; }
    }
}