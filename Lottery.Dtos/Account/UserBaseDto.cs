using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Account
{
    public class UserBaseDto
    {
        public string Id { get; set; }
        public AccountRegistType AccountRegistType { get; set; }

        public string Account { get; set; }
    }
}