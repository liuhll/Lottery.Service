using System;
using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.IdentifyCodes
{
    public class IdentifyCodeDto
    {
        public string Id { get; set; }

        public string Code { get; set; }

        public int MessageType { get; set; }

        public IdentifyCodeType IdentifyCodeType { get; set; }

        public string Receiver { get; set; }
        public DateTime ExpirationDate { get; set; }

        public DateTime? ValidateDate { get; set; }

        public int Status { get; set; }

    }
}