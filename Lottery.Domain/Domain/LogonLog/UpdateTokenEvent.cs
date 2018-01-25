using System;
using ENode.Eventing;

namespace Lottery.Core.Domain.LogonLog
{
    public class UpdateTokenEvent : DomainEvent<string>
    {
        private UpdateTokenEvent()
        {
        }

        public UpdateTokenEvent(string userId, DateTime updateTokenTime, string updateBy)
        {
            UserId = userId;
            UpdateTokenTime = updateTokenTime;
            UpdateBy = updateBy;
        }

        public string UserId { get; private set; }

        public DateTime UpdateTokenTime { get; private set; }

        public string UpdateBy { get; private set; }
    }
}