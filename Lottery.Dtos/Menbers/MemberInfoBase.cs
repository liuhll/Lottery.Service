using Lottery.Infrastructure.Enums;
using System;

namespace Lottery.Dtos.Menbers
{
    public class MemberInfoBase
    {
        public int MemberRank { get; set; }
        public DateTime InvalidDate { get; set; }

        public MemberStatus Status { get; set; }
    }
}