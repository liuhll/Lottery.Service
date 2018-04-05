using System;
using System.Collections.Generic;
using Lottery.Dtos.OnlineHelp;

namespace Lottery.AppService.Operations
{
    public interface IOnlineHelpAppService
    {
        ICollection<OnlineGroupOutput> GetOnlineHelps(string lotteryCode);
    }
}
