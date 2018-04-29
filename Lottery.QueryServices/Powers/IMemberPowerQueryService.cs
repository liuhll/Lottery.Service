using Lottery.Dtos.Power;
using System.Collections.Generic;

namespace Lottery.QueryServices.Powers
{
    public interface IMemberPowerQueryService
    {
        ICollection<PowerGrantInfo> GetMermberPermissions(string lotteryId, int memberRank);
    }
}