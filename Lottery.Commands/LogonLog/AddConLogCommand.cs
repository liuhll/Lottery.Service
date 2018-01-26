using System;
using ENode.Commanding;
using Lottery.Infrastructure.Enums;

namespace Lottery.Commands.LogonLog
{
    public class AddConLogCommand : Command<string>
    {
        private AddConLogCommand()
        {
        }

        public AddConLogCommand(string id,string userId, int clientNo,string systemTypeId, string ip,DateTime invalidDateTime, string createBy) :base(id)
        {
            UserId = userId;
            SystemTypeId = systemTypeId;
            ClientNo = clientNo;
            Ip = ip;
            CreateBy = createBy;
            InvalidDateTime = invalidDateTime;
        }

        public string SystemTypeId { get; private set; }

        public string UserId { get; private set; }

        public int ClientNo { get; private set; }

        public string Ip { get; private set; }

        public string CreateBy { get; private set; }

        public DateTime InvalidDateTime { get; private set; }
    }
}