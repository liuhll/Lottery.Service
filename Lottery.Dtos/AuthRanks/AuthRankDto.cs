using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.AuthRanks
{
    public class AuthRankDto
    {
        public string Id { get; set; }

        public string LotteryId { get; set; }

        public string RoleId { get; set; }

        public MemberRank MemberRank { get; set; }

        public decimal AccountPrice { get; set; }

        public int PointPrice { get; set; }

        public bool EnablePointConsume { get; set; }

        public string Title { get; set; }

        public string Describe { get; set; }

        public string Notes { get; set; }

        public int PermitClientCount { get; set; }

        public bool CanSell { get; set; }

        public int Status { get; set; }
    }
}