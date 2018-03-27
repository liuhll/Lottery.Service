using ENode.Commanding;

namespace Lottery.Commands.UserInfos
{
    public class UpdatePasswordCommand : Command<string>
    {
        private UpdatePasswordCommand()
        {

        }

        public UpdatePasswordCommand(string id, string password, string updateBy) : base(id)
        {
            Password = password;
            UpdateBy = updateBy;
        }

        public string Password { get;private set; }

        public string UpdateBy { get;private set; }
    }
}