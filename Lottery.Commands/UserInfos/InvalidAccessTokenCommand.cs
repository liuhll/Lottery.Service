using ENode.Commanding;

namespace Lottery.Commands.UserInfos
{
    public class InvalidAccessTokenCommand : Command<string>
    {
        private InvalidAccessTokenCommand()
        {
        }

        public InvalidAccessTokenCommand(string id) : base(id)
        {

        }
    }
}