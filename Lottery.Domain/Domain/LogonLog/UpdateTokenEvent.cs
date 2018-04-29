using ENode.Eventing;
using System;

namespace Lottery.Core.Domain.LogonLog
{
    public class UpdateTokenEvent : DomainEvent<string>
    {
        private UpdateTokenEvent()
        {
        }

        public UpdateTokenEvent(DateTime invalidTime, int updateTokenCount, string updateBy)
        {
            UpdateTokenCount = updateTokenCount;
            InvalidTime = invalidTime;
            UpdateBy = updateBy;
        }

        public int UpdateTokenCount { get; private set; }

        public DateTime InvalidTime { get; private set; }

        public string UpdateBy { get; private set; }
    }
}