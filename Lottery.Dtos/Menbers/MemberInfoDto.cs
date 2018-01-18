using System;
using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Menbers
{
    public class MemberInfoDto
    {
        public string UserId { get; set; }

        public string LotteryId { get; set; }

        public string LastAuthOrderId { get; set; }

        public DateTime InvalidDate { get; set; }

        public int Grade { get; set; }

        public MemberStatus Status { get; set; }
    }
}