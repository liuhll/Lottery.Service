using System;
using ENode.Commanding;

namespace Lottery.Commands.UserInfos
{
    public class UpdateAccessTokenCommand : Command<string>
    {
        private UpdateAccessTokenCommand()
        {
        }

        public UpdateAccessTokenCommand(string id, string userId, string accessToken,string updateBy) : base(id)
        {
            UserId = userId;
            AccessToken = accessToken;
            UpdateBy = updateBy;
        }

        public string UserId { get; private set; }

        public string AccessToken { get; private set; }

        public string UpdateBy { get; private set; }
    }
}