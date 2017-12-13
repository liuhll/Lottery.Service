using ECommon.Components;
using Lottery.Dtos.Lotteries;
using Lottery.QueryServices.Lotteries;

namespace Lottery.AppService.Plan
{
    [Component]
    public class PlanInfoAppService : IPlanInfoAppService
    {
        private readonly IPlanInfoQueryService _planInfoQueryService;

        public PlanInfoAppService(IPlanInfoQueryService planInfoQueryService)
        {
            _planInfoQueryService = planInfoQueryService;

        }

        public PlanInfoDto GetPlanInfo(string planCode)
        {
            return _planInfoQueryService.GetPlanInfoByCode(planCode);
        }
    }
}