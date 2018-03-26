using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.UserInfo
{
    public class UserProfileInput
    {
        /// <summary>
        /// 手机号或电子邮件
        /// </summary>
        public string Profile { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Identifycode { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public AccountRegistType ProfileType { get; set; }
    }
}