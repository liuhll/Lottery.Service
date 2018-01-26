using System;
using ENode.Eventing;

namespace Lottery.Core.Domain.LogonLog
{
    public class AddConLogEvent : DomainEvent<string>
    {

        private AddConLogEvent()
        {
        }

        public AddConLogEvent(string userId,int clientNo,string systemTypeId,string ip,DateTime invalidTime, string createBy)
        {
            UserId = userId;
            ClientNo = clientNo;
            SystemTypeId = systemTypeId;
            InvalidTime = invalidTime;
            Ip = ip;
            CreateBy = createBy;
        }

        public string SystemTypeId { get; private set; }

        public string UserId { get; private set; }

        public int ClientNo { get; private set; }

        public string Ip { get; private set; }

        public DateTime InvalidTime { get; private set; }

        public string CreateBy { get; private set; }
    }
}