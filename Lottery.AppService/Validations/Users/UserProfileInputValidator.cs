using System.Text.RegularExpressions;
using ECommon.Components;
using FluentValidation;
using Lottery.Dtos.UserInfo;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Enums;

namespace Lottery.AppService.Validations
{
    [Component]
    public class UserProfileInputValidator : AbstractValidator<UserProfileInput>
    {
        public UserProfileInputValidator()
        {

            // RuleFor(m => m.Profile).NotEmpty().WithMessage("账号不允许为空");
            string message = "{Message}";
            RuleFor(m => m.ProfileType).Must((m, p, context) =>
            {
                if (p == AccountRegistType.UserName)
                {
                    context.MessageFormatter.AppendArgument("Message", "不允许绑定用户名");
                    return false;
                }
                if (p == AccountRegistType.Email)
                {
                    if (!Regex.IsMatch(m.Profile,RegexConstants.Email))
                    {
                        context.MessageFormatter.AppendArgument("Message", "电子邮件格式不合法");
                        return false;
                    }
                    return true;
                }
                if (p == AccountRegistType.Phone)
                {
                    if (!Regex.IsMatch(m.Profile, RegexConstants.Phone))
                    {
                        context.MessageFormatter.AppendArgument("Message", "手机格式不合法");
                        return false;
                    }
                    return true;
                }
                context.MessageFormatter.AppendArgument("Message", "账号不合法");
                return false;
            }).WithMessage(message);
        }
    }
}