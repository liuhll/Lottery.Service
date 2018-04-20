using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.UserInfo
{
    public class UserInfoInput
    {
        /// <summary>
        /// 账号(用户名/邮箱/手机号)
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string IdentifyCode { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 客户端
        /// </summary>
        public ClientRegistType ClientRegistType { get; set; }
    }
}