namespace Lottery.WebApi.ViewModels
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
    }
}