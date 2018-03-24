namespace Lottery.Dtos.IdentifyCodes
{
    public class IdentifyCodeValidOutput
    {
        public string IdentifyCodeId { get; set; }

        public bool IsValid { get; set; }

        public bool IsOvertime { get; set; }
    }
}