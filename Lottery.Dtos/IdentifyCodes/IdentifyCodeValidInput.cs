using System;
using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.IdentifyCodes
{
    public class IdentifyCodeValidInput
    {

        public string Account { get; set; }

        public string IdentifyCode { get; set; }
    }
}
