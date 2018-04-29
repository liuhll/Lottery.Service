using Lottery.Dtos.Plans;
using System.Collections.Generic;

namespace Lottery.QueryServices.Lotteries
{
    public interface INormGroupQueryService
    {
        ICollection<NormGroupOutput> GetNormGroups(string lotteryId);
    }
}