using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Infrastructure.Exceptions
{
    public class LotteryAuthorizationException : LotteryException
    {
        public LotteryAuthorizationException(string errorMessage,int errorCode = Infrastructure.ErrorCode.AuthorizationFailed) : base(errorMessage, errorCode)
        {
        }
    }
}
