﻿using System.Text.RegularExpressions;
using ECommon.Components;
using FluentValidation;
using Lottery.Infrastructure;
using Lottery.WebApi.ViewModels;

namespace Lottery.WebApi.Validations
{
    [Component]
    public class UserInfoInputValidator : AbstractValidator<UserInfoInput>
    {
        public UserInfoInputValidator()
        {
            RuleFor(m => m.Account).NotEmpty().NotNull().WithMessage("账号不允许为空");
            RuleFor(m => m.Account).Must(BeAValidAccount).WithMessage("账号不合法");
            RuleFor(m => m.Password).NotEmpty().NotNull().WithMessage("密码不允许为空");

        }

        private bool BeAValidAccount(string account)
        {
            return Regex.IsMatch(account,RegexConstants.UserName) ||
                   Regex.IsMatch(account, RegexConstants.Email) ||
                   Regex.IsMatch(account, RegexConstants.Phone);
        }
    }
}