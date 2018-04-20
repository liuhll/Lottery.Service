namespace Lottery.Infrastructure.Exceptions
{
    public class LotteryAuthorizationException : LotteryException
    {
        public LotteryAuthorizationException(string errorMessage, int errorCode = Infrastructure.ErrorCode.AuthorizationFailed) : base(errorMessage, errorCode)
        {
        }
    }
}