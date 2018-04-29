namespace Lottery.Infrastructure.Exceptions
{
    public class ValidationException : LotteryException
    {
        public ValidationException(string errorMessage, int errorCode = Infrastructure.ErrorCode.InvalidToken) : base(errorMessage, errorCode)
        {
        }
    }
}