using ECommon.Components;
using FluentValidation;
using Lottery.Dtos.Norms;
using Lottery.QueryServices.Lotteries;

namespace Lottery.WebApi.Validations
{
    [Component]
    public class UserNormConfigInputValidator : AbstractValidator<UserNormDefaultConfigInput>
    {
        private readonly ILotteryQueryService _lotteryQueryService;
        public UserNormConfigInputValidator()
        {
            _lotteryQueryService = ObjectContainer.Resolve<ILotteryQueryService>();

            RuleFor(p => p.LotteryId).NotEmpty().Must(p =>
            {
                var lotteryInfo = _lotteryQueryService.GetLotteryInfoById(p);
                if (lotteryInfo == null)
                {
                    return false;
                }
                return true;
            }).WithMessage("不存在该彩种");
            RuleFor(p => p.PlanCycle).GreaterThan(0).WithMessage("计划周期必须大于0").LessThanOrEqualTo(10).WithMessage("计划周期必须小于等于10");
            RuleFor(p => p.ForecastCount).GreaterThan(0).WithMessage("定码个数必须大于0").LessThanOrEqualTo(10)
                .WithMessage("定码格式必须小于等于10");
            RuleFor(p => p.UnitHistoryCount).GreaterThanOrEqualTo(10).WithMessage("历史数据期数必须大于等于10");
            RuleFor(p => p.LookupPeriodCount).GreaterThanOrEqualTo(10).WithMessage("计划追号期数必须大于等于10")
                .LessThanOrEqualTo(50).WithMessage("计划追号期数必须小于等于50");
            RuleFor(p => p.MinErrortSeries).Must((p, q) =>
            {
                if (p.MaxErrortSeries == 10 && p.MinErrortSeries == 10)
                {
                    return false;
                }
                if (p.MinErrortSeries < 0 || p.MinRightSeries < 0)
                {
                    return false;
                }
                if (p.MaxErrortSeries >10 || p.MaxRightSeries >10)
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