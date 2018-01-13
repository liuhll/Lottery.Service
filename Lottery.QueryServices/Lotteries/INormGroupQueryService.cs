using System.Collections.Generic;
using Lottery.Dtos.Plans;

namespace Lottery.QueryServices.Lotteries
{
    public interface INormGroupQueryService
    {
        ICollection<NormGroupOutput> GetNormGroups(string lotteryId);
    }
}