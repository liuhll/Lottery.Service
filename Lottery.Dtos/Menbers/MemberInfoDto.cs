using System;

namespace Lottery.Dtos.Menbers
{
    public class MemberInfoDto : MemberInfoBase
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string LotteryId { get; set; }

        public string LastAuthOrderId { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}