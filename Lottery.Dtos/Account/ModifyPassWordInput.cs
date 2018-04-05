namespace Lottery.Dtos.Account
{
    public class ModifyPassWordInput
    {
        public string Account { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
      
    }
}