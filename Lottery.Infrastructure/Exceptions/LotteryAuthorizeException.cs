namespace Lottery.Infrastructure.Exceptions
{
    public class LotteryAuthorizeException : LotteryException
    {
        public LotteryAuthorizeException(string errorMessage, int errorCode = Infrastructure.ErrorCode.AuthorizeFailed) : base(errorMessage,errorCode)
        {
        }

        
    }
}