using ENode.Commanding;

namespace Lottery.Commands.LogonLog
{
    public class AddLogonLogCommand : Command<string>
    {
        private AddLogonLogCommand()
        {
        }

        public AddLogonLogCommand(string id,string userId, string createBy) : base(id)
        {
            UserId = userId;
            CreateBy = createBy;
        }

        public string UserId { get; private set; }

        public string CreateBy { get; private set; }
    }
}