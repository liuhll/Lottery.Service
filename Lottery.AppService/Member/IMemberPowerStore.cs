using Lottery.Dtos.Power;
using System.Collections.Generic;

namespace Lottery.AppService.Member
{
    public interface IMemberPowerStore
    {
        ICollection<PowerGrantInfo> GetMermberPermissions(string lotteryId, int memberRank);
    }
}