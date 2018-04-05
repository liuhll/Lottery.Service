using ECommon.Components;
using FluentValidation;
using Lottery.Dtos.Opinions;
using Lottery.Infrastructure.Extensions;

namespace Lottery.AppService.Validations.Opinions
{
    [Component]
    public class OpinionInputValidtor : AbstractValidator<OpinionInput>
    {
        public OpinionInputValidtor()
        {
            RuleFor(p => p.Content).Must(p => !p.IsNullOrEmpty()).WithMessage("意见不允许为空");
            RuleFor(p => p.ContactWay).Must(p => !p.IsNullOrEmpty()).WithMessage("联系方式不允许为空");
        }
    }
}