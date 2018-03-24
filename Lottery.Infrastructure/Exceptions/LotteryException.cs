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

        public LotteryException(string errorMessage,Exception exception) : base(errorMessage, exception)
        {
            ErrorCode = LotteryErrorCode.SendMessageError;
        }

        public int ErrorCode { get; private set; }
    }
}