namespace Lottery.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveUnderLine(this string str)
        {
            return str.Replace("_", "");
        }

        public static string RemoveStrike(this string str)
        {
            return str.Replace("-", "");
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    }
}