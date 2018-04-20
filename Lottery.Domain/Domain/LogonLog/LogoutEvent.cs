using ENode.Eventing;
using System;

namespace Lottery.Core.Domain.LogonLog
{
    public class LogoutEvent : DomainEvent<string>
    {
        private LogoutEvent()
        {
        }

        public LogoutEvent(string userId, DateTime logoutTime, int onlineTime)
        {
            UserId = userId;
            OnlineTime = onlineTime;
            LogoutTime = logoutTime;
        }

        public string UserId { get; private set; }

        public int OnlineTime { get; private set; }

        public DateTime LogoutTime { get; private set; }
    }
}