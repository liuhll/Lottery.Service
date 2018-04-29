namespace Lottery.Infrastructure.Exceptions
{
    public class AccessTokenException : LotteryException
    {
        public AccessTokenException(string errorMessage, int errorCode = Infrastructure.ErrorCode.InvalidToken) : base(errorMessage, errorCode)
        {
        }
    }
}