using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Account
{
    public class UserInfoViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public AccountRegistType AccountRegistType { get; set; }
    }
}