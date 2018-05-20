using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.RunApp.Jobs
{
    public class JssyxwJob : RunLotteryAbstractJob
    {
        protected override void PreInitialize()
        {
            _lotteryCode = "JSSYXW";
        }

        protected override void PostinItialize()
        {
            
        }
    }
}
