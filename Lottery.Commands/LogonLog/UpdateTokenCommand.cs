using System;
using ENode.Commanding;

namespace Lottery.Commands.LogonLog
{
    public class UpdateTokenCommand : Command<string>
    {
        private UpdateTokenCommand()
        {
        }

        public UpdateTokenCommand(string id,DateTime invalidTime, string updateBy):base(id)
        {
            InvalidTime = invalidTime;
            UpdateBy = updateBy;
        }


        public DateTime InvalidTime { get; private set; }

        public string UpdateBy { get; private set; }
    }
}