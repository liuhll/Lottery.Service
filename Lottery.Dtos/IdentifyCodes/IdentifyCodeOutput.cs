using System;

namespace Lottery.Dtos.IdentifyCodes
{
    public class IdentifyCodeOutput
    {
        public string IdentifyCodeId { get; set; }

        public string Code { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsNew { get; set; }
    }
}