using System.Collections.Generic;
using ECommon.Components;
using Lottery.Dtos.Lotteries;
using Lottery.QueryServices.Lotteries;

namespace Lottery.AppService.Norm
{
    [Component]
    public class NormConfigAppService : INormConfigAppService
    {
        private readonly INormConfigQueryService _normConfigQueryService;

        public NormConfigAppService(INormConfigQueryService normConfigQueryService)
        {
            _normConfigQueryService = normConfigQueryService;
        }

        public ICollection<NormConfigDto> GetNormConfigsByUserIdOrDefault(string userId = "")
        {
            return _normConfigQueryService.GetUserOrDefaultNormConfigs(userId);
        }
    }
}