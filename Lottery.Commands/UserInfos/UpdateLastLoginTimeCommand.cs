using ENode.Commanding;

namespace Lottery.Commands.UserInfos
{
    public class UpdateLastLoginTimeCommand : Command<string>
    {
        private UpdateLastLoginTimeCommand()
        {
        }

        public UpdateLastLoginTimeCommand(string userId) : base(userId)
        {
        }
    }
}