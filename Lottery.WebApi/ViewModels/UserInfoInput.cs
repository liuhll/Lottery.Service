using System.Text.RegularExpressions;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;

namespace Lottery.WebApi.ViewModels
{
    public class UserInfoInput
    {
        /// <summary>
        /// 账号(用户名/邮箱/手机号)
        /// </summary>
        public string Account { get; set; }

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