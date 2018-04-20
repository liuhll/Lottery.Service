using ENode.Commanding;

namespace Lottery.Commands.UserInfos
{
    public class BindUserEmailCommand : Command<string>
    {
        private BindUserEmailCommand()
        {
        }

        public BindUserEmailCommand(string id, string email) : base(id)
        {
            Email = email;
        }

        public string Email { get; private set; }
    }
}