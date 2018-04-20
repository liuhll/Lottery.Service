namespace Lottery.Infrastructure.Exceptions
{
    public class LotteryDataException : LotteryException
    {
        public LotteryDataException(string errorMessage, int errorCode = Infrastructure.ErrorCode.DataError) : base(errorMessage, errorCode)
        {
        }
    }
}