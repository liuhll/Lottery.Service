using ECommon.Components;
using FluentValidation;
using Lottery.Dtos.Norms;

namespace Lottery.AppService.Validations
{
    [Component]
    public class UserNormConfigInputValidator : AbstractValidator<UserNormDefaultConfigInput>
    {
        public UserNormConfigInputValidator()
        {
            RuleFor(p => p.PlanCycle).GreaterThan(0).WithMessage("计划周期必须大于0").LessThanOrEqualTo(10).WithMessage("计划周期必须小于等于10");
            RuleFor(p => p.ForecastCount).GreaterThan(0).WithMessage("定码个数必须大于0").LessThanOrEqualTo(10)
                .WithMessage("定码格式必须小于等于10");
            RuleFor(p => p.UnitHistoryCount).GreaterThanOrEqualTo(10).WithMessage("历史数据期数必须大于等于10");
            RuleFor(p => p.LookupPeriodCount).GreaterThanOrEqualTo(10).WithMessage("计划追号期数必须大于等于10")
                .LessThanOrEqualTo(50).WithMessage("计划追号期数必须小于等于50");
            RuleFor(p => p.MinErrorSeries).Must((p, q) =>
            {
                if (p.MaxErrorSeries == 10 && p.MinErrorSeries == 10)
                {
                    return false;
                }
                if (p.MinErrorSeries < 0 || p.MinRightSeries < 0)
                {
                    return false;
                }
                if (p.MaxErrorSeries > 10 || p.MaxRightSeries > 10)
                {
                    return false;
                }
                if (p.MaxRightSeries == 0 && p.MinRightSeries == 0)
                {
                    return false;
                }
                if (p.ExpectMinScore == 0 && p.ExpectMaxScore == 0)
                {
                    return false;
                }
                if (p.ExpectMinScore < 0 || p.ExpectMaxScore > 100)
                {
                    return false;
                }
                return true;
            }).WithMessage("无效的计划指标");
        }
    }
}