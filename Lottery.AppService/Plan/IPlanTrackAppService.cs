using Lottery.Dtos.Lotteries;

namespace Lottery.AppService.Plan
{
    public interface IPlanTrackAppService
    {
        PlanTrackDetail GetPlanTrackDetail(NormConfigDto userNorm,string lotteryCode,string userId);
    }
}