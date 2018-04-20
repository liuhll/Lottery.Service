using ENode.Commanding;

namespace Lottery.Commands.UserInfos
{
    public class BindUserPhoneCommand : Command<string>
    {
        private BindUserPhoneCommand()
        {
        }

        public BindUserPhoneCommand(string id, string phone) : base(id)
        {
            Phone = phone;
        }

        public string Phone { get; private set; }
    }
}