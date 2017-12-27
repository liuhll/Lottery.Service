using System;

namespace Lottery.Infrastructure.Exceptions
{
    public class LotteryException : Exception
    {
        public LotteryException(string errorMessage) : base(errorMessage)
        {

        }
    }
}