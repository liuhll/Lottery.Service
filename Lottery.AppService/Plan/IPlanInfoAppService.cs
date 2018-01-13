using Lottery.Dtos.Lotteries;
using Lottery.Dtos.Plans;

namespace Lottery.AppService.Plan
{
    public interface IPlanInfoAppService
    {
        PlanInfoDto GetPlanInfo(string planCode);

        UserPlanInfoDto GetUserPlanInfo(string lotteryId, string userId);
    }
}