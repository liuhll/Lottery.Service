using System;
using ENode.Eventing;

namespace Lottery.Core.Domain.IdentifyCode
{
    public class InvalidIdentifyCodeEvent : DomainEvent<string>
    {
        private InvalidIdentifyCodeEvent()
        {
        }

        public InvalidIdentifyCodeEvent(string receiver,string updateBy)
        {
            ValidateDate = DateTime.Now;
            Status = 1;
            UpdateBy = updateBy;
            Receiver = receiver;
        }

        public DateTime ValidateDate { get; private set; }

        public string Receiver { get; private set; }

        public int Status { get; private set; }

        public string UpdateBy { get; private set; }
    }
}