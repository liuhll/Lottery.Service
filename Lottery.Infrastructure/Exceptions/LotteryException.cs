using System;
using LotteryErrorCode = Lottery.Infrastructure.ErrorCode;

namespace Lottery.Infrastructure.Exceptions
{
    public class LotteryException : Exception
    {
        public LotteryException(string errorMessage, int errorCode = LotteryErrorCode.BusinessError) : base(errorMessage)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; private set; }
    }
}