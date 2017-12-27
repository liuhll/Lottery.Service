using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lottery.WebApi.RunTime.Session
{
    public interface ILotterySession
    {

        /// <summary>
        /// Gets current UserId or null.
        /// It can be null if no user logged in.
        /// </summary>
        string UserId { get; }

        string UserName { get; }
    }
}