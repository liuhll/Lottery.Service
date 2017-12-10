using System.Collections.Generic;
using Lottery.Dtos.Lotteries;

namespace Lottery.QueryServices.Lotteries
{
    public interface INormConfigQueryService
    {
        ICollection<NormConfigDto> GetDefaultNormConfigs();

        ICollection<NormConfigDto> GetUserOrDefaultNormConfigs(string userId = "");

        ICollection<NormConfigDto> GetUserNormConfig(string userId);

        ICollection<NormConfigDto> GetPlanConfigDtos(string planId);
    }
}