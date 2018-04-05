using System.Collections.Generic;
using Lottery.Dtos.OnlineHelp;

namespace Lottery.QueryServices.OnlineHelps
{
    public interface IOnlineHelpQueryService
    {
        ICollection<dynamic> GetOnlineHelps(string lotteryCode);
    }
}