using System;
using ENode.Commanding;

namespace Lottery.Commands.LogonLog
{
    public class LogoutCommand : Command<string>
    {
        private LogoutCommand()
        {
        }

        public LogoutCommand(string userId, string updateBy) : base(userId)
        {
            UserId = userId;
            UpdateBy = updateBy;
        }

        public string UserId { get; private set; }


        public string UpdateBy { get; private set; }
    }
}