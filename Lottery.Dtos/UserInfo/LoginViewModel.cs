namespace Lottery.Dtos.UserInfo
{
    /// <summary>
    /// Login ViewModel
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// 账号(用户名、电子邮件、电话)
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        public bool IsForce { get; set; }

        /// <summary>
        /// 系统类型
        /// </summary>
        public string SystemType { get; set; }

    }
}