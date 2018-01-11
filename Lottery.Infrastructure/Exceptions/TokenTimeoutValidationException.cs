namespace Lottery.Infrastructure.Exceptions
{
    public class TokenTimeoutValidationException : LotteryException
    {
        public TokenTimeoutValidationException(string errorMessage) : base(errorMessage)
        {
        }
    }
}