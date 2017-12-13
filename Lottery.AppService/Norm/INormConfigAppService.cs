using System.Collections.Generic;
using Lottery.Dtos.Lotteries;

namespace Lottery.AppService.Norm
{
    public interface INormConfigAppService
    {
        ICollection<NormConfigDto> GetNormConfigsByUserIdOrDefault(string userId = "");
    }
}