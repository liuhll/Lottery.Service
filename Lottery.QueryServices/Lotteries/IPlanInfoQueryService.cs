using Lottery.Dtos.Lotteries;
using System.Collections.Generic;

namespace Lottery.QueryServices.Lotteries
{
    public interface IPlanInfoQueryService
    {
        PlanInfoDto GetPlanInfoByCode(string planCode);

        PlanInfoDto GetPlanInfoById(string planId);

        ICollection<PlanInfoDto> GetAll();

        ICollection<PlanInfoDto> GetPlanInfoByLotteryId(string lotteryId);
    }
}