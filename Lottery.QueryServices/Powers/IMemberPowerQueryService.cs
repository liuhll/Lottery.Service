using System.Collections.Generic;
using Lottery.Dtos.Power;

namespace Lottery.QueryServices.Powers
{
    public interface IMemberPowerQueryService
    {
        ICollection<PowerGrantInfo> GetMermberPermissions(string lotteryId, int memberRank);
    }
}