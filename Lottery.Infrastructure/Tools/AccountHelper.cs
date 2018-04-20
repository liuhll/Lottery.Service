using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;
using System.Text.RegularExpressions;

namespace Lottery.Infrastructure.Tools
{
    public static class AccountHelper
    {
        public static AccountRegistType JudgeAccountRegType(string account)
        {
            if (Regex.IsMatch(account, RegexConstants.UserName))
            {
                return AccountRegistType.UserName;
            }
            if (Regex.IsMatch(account, RegexConstants.Email))
            {
                return AccountRegistType.Email;
            }
            if (Regex.IsMatch(account, RegexConstants.Phone))
            {
                return AccountRegistType.Phone;
            }
            throw new LotteryDataException("注册账号不合法");
        }
    }
}