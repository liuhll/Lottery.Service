using System.Collections.Generic;
using Lottery.Dtos.Power;

namespace Lottery.AppService.Member
{
    public interface IMemberPowerStore
    {
        ICollection<PowerGrantInfo> GetMermberPermissions(string lotteryId, int memberRank);
    }
}