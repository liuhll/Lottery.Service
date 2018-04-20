using ENode.Commanding;
using System;

namespace Lottery.Commands.IdentifyCodes
{
    public class UpdateIdentifyCodeCommand : Command<string>
    {
        private UpdateIdentifyCodeCommand()
        {
        }

        public UpdateIdentifyCodeCommand(string id, string code, string receiver, DateTime expirationDate, string updateBy) : base(id)
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