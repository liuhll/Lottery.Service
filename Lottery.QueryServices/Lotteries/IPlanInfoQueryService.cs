using System.Collections.Generic;
using Lottery.Dtos.Lotteries;

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