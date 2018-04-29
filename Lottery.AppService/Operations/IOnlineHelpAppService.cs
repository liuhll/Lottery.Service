using Lottery.Dtos.OnlineHelp;
using System.Collections.Generic;

namespace Lottery.AppService.Operations
{
    public interface IOnlineHelpAppService
    {
        ICollection<OnlineGroupOutput> GetOnlineHelps(string lotteryCode);
    }
}