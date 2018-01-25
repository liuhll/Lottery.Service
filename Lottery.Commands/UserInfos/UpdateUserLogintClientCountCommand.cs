using ENode.Commanding;

namespace Lottery.Commands.UserInfos
{
    public class UpdateUserLogintClientCountCommand : Command<string>
    {
        private UpdateUserLogintClientCountCommand()
        {
        }

        public UpdateUserLogintClientCountCommand(string userId,bool isLogin) : base(userId)
        {
            UserId = userId;
            IsLogin = isLogin;
        }

        public string UserId { get; private set; }

        public bool IsLogin { get; private set; }


    }
}