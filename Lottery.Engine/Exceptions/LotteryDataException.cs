using System;

namespace Lottery.Engine.Exceptions
{
    public class LotteryDataException : LotteryException
    {
        public LotteryDataException(string errorMessage) : base(errorMessage)
        {
        }
    }
}