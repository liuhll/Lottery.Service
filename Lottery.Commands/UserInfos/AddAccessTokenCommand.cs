using ENode.Commanding;

namespace Lottery.Commands.UserInfos
{
    public class AddAccessTokenCommand : Command<string>
    {


        private AddAccessTokenCommand()
        {
        }

        public AddAccessTokenCommand(string id, string userId, string accessToken,string createBy) : base(id)
        {
            UserId = userId;
            AccessToken = accessToken;
            CreateBy = createBy;
        }

        public string UserId { get; private set; }

        public string AccessToken { get; private set; }

        public string CreateBy { get; private set; }
    }
}