namespace Lottery.Infrastructure.Exceptions
{
    public class LotteryDataException : LotteryException
    {
        public LotteryDataException(string errorMessage) : base(errorMessage)
        {
        }
    }
}