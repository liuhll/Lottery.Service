using System;
using ENode.Commanding;

namespace Lottery.Commands.LogonLog
{
    public class UpdateTokenCommand : Command<string>
    {
        private UpdateTokenCommand()
        {
        }

        public UpdateTokenCommand(string userId, DateTime updateTokenTime, string updateBy)
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