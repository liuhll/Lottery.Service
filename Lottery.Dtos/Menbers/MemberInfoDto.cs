using System;
using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Menbers
{
    public class MemberInfoDto : MemberInfoBase
    {
        public string UserId { get; set; }

        public string LotteryId { get; set; }

        public string LastAuthOrderId { get; set; }
    
    }
}