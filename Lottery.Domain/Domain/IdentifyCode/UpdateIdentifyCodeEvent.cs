using System;
using ENode.Eventing;

namespace Lottery.Core.Domain.IdentifyCode
{
    public class UpdateIdentifyCodeEvent : DomainEvent<string>
    {
        private UpdateIdentifyCodeEvent()
        {
        }

        public UpdateIdentifyCodeEvent(string code,string receiver, DateTime expirationDate,string updateBy)
        {
            Code = code;
            ExpirationDate = expirationDate;
            UpdateBy = updateBy;
            Receiver = receiver;
        }

        public string Receiver { get; private set; }

        public string Code { get; private set; }

        public DateTime ExpirationDate { get; private set; }

        public string UpdateBy { get; private set; }
    }
}