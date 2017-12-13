using Lottery.Dtos.Lotteries;

namespace Lottery.AppService.Plan
{
    public interface IPlanInfoAppService
    {
        PlanInfoDto GetPlanInfo(string planCode);
    }
}