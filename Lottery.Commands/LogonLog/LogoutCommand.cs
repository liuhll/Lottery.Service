using ENode.Commanding;

namespace Lottery.Commands.LogonLog
{
    public class LogoutCommand : Command<string>
    {
        private LogoutCommand()
        {
        }

        public LogoutCommand(string conLogId, string updateBy) : base(conLogId)
        {
            UpdateBy = updateBy;
        }

        public string UpdateBy { get; private set; }
    }
}