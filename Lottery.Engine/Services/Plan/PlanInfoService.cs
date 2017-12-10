using ECommon.Components;
using Lottery.Dtos.Lotteries;
using Lottery.QueryServices.Lotteries;

namespace Lottery.Engine.Services
{
    [Component]
    public class PlanInfoService : IPlanInfoService
    {
        private readonly IPlanInfoQueryService _planInfoQueryService;

        public PlanInfoService(IPlanInfoQueryService planInfoQueryService)
        {
            _planInfoQueryService = planInfoQueryService;
          
        }

        public PlanInfoDto GetPlanInfo(string planCode)
        {
            return _planInfoQueryService.GetPlanInfoByCode(planCode);
        }
    }
}