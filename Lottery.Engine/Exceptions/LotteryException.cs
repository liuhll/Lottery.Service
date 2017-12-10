using System;

namespace Lottery.Engine.Exceptions
{
    public class LotteryException : Exception
    {
        public LotteryException(string errorMessage) : base(errorMessage)
        {

        }
    }
}