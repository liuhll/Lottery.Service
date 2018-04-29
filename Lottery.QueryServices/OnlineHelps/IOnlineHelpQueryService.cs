using System.Collections.Generic;

namespace Lottery.QueryServices.OnlineHelps
{
    public interface IOnlineHelpQueryService
    {
        ICollection<dynamic> GetOnlineHelps(string lotteryCode);
    }
}