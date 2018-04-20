using ECommon.Components;
using Lottery.Dtos.Lotteries;
using Lottery.Dtos.Plans;
using Lottery.Infrastructure.Collections;
using Lottery.QueryServices.Lotteries;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.AppService.Plan
{
    [Component]
    public class PlanInfoAppService : IPlanInfoAppService
    {
        private readonly IPlanInfoQueryService _planInfoQueryService;
        private readonly INormConfigQueryService _normConfigQueryService;
        private readonly INormGroupQueryService _normGroupQueryService;

        public PlanInfoAppService(IPlanInfoQueryService planInfoQueryService,
            INormConfigQueryService normConfigQueryService,
            INormGroupQueryService normGroupQueryService)
        {
            _planInfoQueryService = planInfoQueryService;
            _normConfigQueryService = normConfigQueryService;
            _normGroupQueryService = normGroupQueryService;
        }

        public PlanInfoDto GetPlanInfo(string planCode)
        {
            return _planInfoQueryService.GetPlanInfoByCode(planCode);
        }

        public PlanInfoDto GetPlanInfoById(string planId)
        {
            return _planInfoQueryService.GetPlanInfoById(planId);
        }

        public UserPlanInfoDto GetUserPlanInfo(string lotteryId, string userId)
        {
            var userSelectedUserPlanInfo = new List<PlanInfoOutput>();
            var allPlanInfos = _normGroupQueryService.GetNormGroups(lotteryId);
            var userPlanConfigs = _normConfigQueryService.GetUserOrDefaultNormConfigs(lotteryId, userId);

            foreach (var normGroup in allPlanInfos)
            {
                foreach (var planInfo in normGroup.PlanInfos)
                {
                    if (userPlanConfigs.Any(p => p.PlanId == planInfo.Id))
                    {
                        planInfo.IsSelected = true;
                        userSelectedUserPlanInfo.AddIfNotContains(planInfo);
                    }
                }
            }

            return new UserPlanInfoDto()
            {
                UserSelectedPlanInfos = userSelectedUserPlanInfo,
                AllPlanInfos = allPlanInfos
            };
        }
    }
}