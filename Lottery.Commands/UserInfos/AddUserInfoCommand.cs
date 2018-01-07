using ENode.Commanding;
using Lottery.Infrastructure.Enums;

namespace Lottery.Commands.UserInfos
{
    public class AddUserInfoCommand : Command<string>
    {
        private AddUserInfoCommand()
        {
        }

        public AddUserInfoCommand(string id,string account,string password,ClientRegistType clientRegistType,AccountRegistType accountRegistType) : base(id)
        {
            switch (accountRegistType)
            {
                case AccountRegistType.UserName:
                    UserName = account;
                    break;
                case AccountRegistType.Email:
                    Email = account;
                    break;
                case AccountRegistType.Phone:
                    Phone = account;
                    break;
            }
            AccountRegistType = accountRegistType;
            Password = password;
            ClientRegistType = clientRegistType;

        }

        public string UserName { get; private set; }

        public string Email { get; private set; }

        public string Phone { get; private set; }

        public string Password { get; private set; }

        public ClientRegistType ClientRegistType { get; private set; }

        public AccountRegistType AccountRegistType { get; private set; }
    }
}