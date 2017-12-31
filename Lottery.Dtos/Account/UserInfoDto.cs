using System;

namespace Lottery.Dtos.Account
{
    public class UserInfoDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool IsActive { get; set; }

        public DateTime LastLoginTime { get; set; }

        public string QQCode { get; set; }

        public string Wechat { get; set; }

        public int UserRegistType { get; set; }
    }
}