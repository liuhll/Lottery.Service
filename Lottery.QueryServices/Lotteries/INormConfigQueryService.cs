using System.Collections.Generic;
using Lottery.Dtos.Lotteries;
using Lottery.Dtos.Norms;

namespace Lottery.QueryServices.Lotteries
{
    public interface INormConfigQueryService
    {
        ICollection<NormConfigDto> GetDefaultNormConfigs(string lotteryId);

        ICollection<NormConfigDto> GetUserOrDefaultNormConfigs(string lotteryId,string userId = "");

        ICollection<NormConfigDto> GetUserNormConfigs(string lotteryId,string userId);

        NormConfigDto GetUserNormConfig(string nromId);

        //ICollection<NormConfigDto> GetPlanConfigDtos(string planId);

        UserPlanNormOutput GetUserNormConfigById(string userId, string normId);
        UserPlanNormOutput GetUserNormConfigByPlanId(string userId, string lotteryId, string planId);
        PlanInfoDto GetNormPlanInfoByNormId(string normId,string lotteryId);
    }
}