using System.Collections.Generic;
using Lottery.Dtos.Lotteries;
using Lottery.Dtos.Norms;

namespace Lottery.AppService.Norm
{
    public interface INormConfigAppService
    {
        ICollection<NormConfigDto> GetNormConfigsByUserIdOrDefault(string userId = "");

        UserNormDefaultConfigDto GetUserNormDefaultConfig(string userId, string lotteryId);
    }
}