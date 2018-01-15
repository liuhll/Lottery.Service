using System.Linq;
using ECommon.Components;
using FluentValidation;
using Lottery.Dtos.Plans;
using Lottery.QueryServices.Lotteries;

namespace Lottery.WebApi.Validations
{
    [Component]
    public class UserPlanInfoInputValidator :AbstractValidator<UserPlanInfoInput>
    {
        private readonly ILotteryQueryService _lotteryQueryService;
        public UserPlanInfoInputValidator()
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

            RuleFor(p => p.PlanIds).Must(p =>
            {

                if (p == null || p.Count <= 0)
                {
                    return false;
                }
                return true;
            }).WithMessage("至少选择一个计划");
        }
    }
}