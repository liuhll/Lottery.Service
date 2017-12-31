namespace Lottery.Infrastructure.Exceptions
{
    public class ValidationException : LotteryException
    {
        public ValidationException(string errorMessage) : base(errorMessage)
        {
        }
    }
}