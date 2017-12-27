using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lottery.WebApi.RunTime.Session
{
    public abstract class LotterySessionBase : ILotterySession
    {
        public abstract string UserId { get; }
        public abstract string UserName { get; }
    }
}