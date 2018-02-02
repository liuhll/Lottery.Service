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
        public UserPlanInfoInputValidator()
        {
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