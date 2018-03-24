using System;
using ENode.Commanding;

namespace Lottery.Commands.IdentifyCodes
{
    public class InvalidIdentifyCodeCommand : Command<string>
    {
        private InvalidIdentifyCodeCommand()
        {
        }

        public InvalidIdentifyCodeCommand(string id, string receiver, string updateBy):base(id)
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