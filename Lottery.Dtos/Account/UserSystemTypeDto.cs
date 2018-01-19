using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Account
{
    public class UserSystemTypeDto
    {
        public string UserId { get; set; }

        public SystemType SystemType { get; set; }

        public int Status { get; set; }
    }
}