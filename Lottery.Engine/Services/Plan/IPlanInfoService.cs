using Lottery.Dtos.Lotteries;

namespace Lottery.Engine.Services
{
    public interface IPlanInfoService
    {
        PlanInfoDto GetPlanInfo(string planCode);
    }
}