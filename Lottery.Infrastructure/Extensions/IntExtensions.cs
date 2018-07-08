namespace Lottery.Infrastructure.Extensions
{
    public static class IntExtensions
    {
        public static bool IsPrime(this int num)
        {
            if (num > 1)
            {
                for (int i = 2; i < num; i++)
                {
                    if (num % i == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}