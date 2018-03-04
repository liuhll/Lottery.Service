using ECommon.Components;
using FluentValidation;
using Lottery.Dtos.Plans;
using Lottery.Infrastructure.RunTime.Session;
using Lottery.QueryServices.Lotteries;
using System.Linq;

namespace Lottery.AppService.Validations
{
    [Component]
    public class UserPlanInfoInputValidator :AbstractValidator<UserPlanInfoInput>
    {
        private readonly IPlanInfoQueryService _planInfoQueryService;
        private readonly ILotterySession _lotterySession;
        public UserPlanInfoInputValidator()
        {
            _planInfoQueryService = ObjectContainer.Resolve<IPlanInfoQueryService>();
            _lotterySession = NullLotterySession.Instance;

            var planInfos = _planInfoQueryService.GetPlanInfoByLotteryId(_lotterySession.SystemTypeId);
            var allPlanIds = planInfos.Select(p => p.Id).ToList();


            RuleFor(p => p.PlanIds).Must(p =>
            {
                if (p == null || p.Count <= 0)
                {
                    return false;
                }
               
                return true;
            }).WithMessage("至少选择一个计划");

            RuleFor(p => p.PlanIds).Must(p => {
                if (p.Count != p.Distinct().Count()) {
                    return false;
                }
                return true;
            }).WithMessage("不允许选择重复的计划");

            RuleFor(p => p.PlanIds).Must(p=> {

                if (p.All(t => allPlanIds.Any(b => b == t.PlanId))) {
                    return true;
                }
                return false;
            }).WithMessage("计划选择错误,该彩种在系统中包含这类型的计划");
        }
    }
}