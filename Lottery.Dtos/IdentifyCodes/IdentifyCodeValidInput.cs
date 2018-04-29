namespace Lottery.Dtos.IdentifyCodes
{
    public class IdentifyCodeValidInput
    {
        public string Account { get; set; }

        public string IdentifyCode { get; set; }

        public bool IsValidAccountExist { get; set; }
    }
}