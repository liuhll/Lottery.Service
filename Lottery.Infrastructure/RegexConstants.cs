namespace Lottery.Infrastructure
{
    public static class RegexConstants
    {
        //        1 数字：^[0-9]*$
        public const string Number = "^[0-9]*$";

        public const string NumberNbit = @"^\d{n}$";

        public const string UserName = "^[a-zA-Z][a-zA-Z0-9_]{4,15}$";

        public const string Email = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        public const string Phone = @"^(13[0-9]|14[0-9]|15[0-9]|166|17[0-9]|18[0-9]|19[8|9])\d{8}$";
    }
}