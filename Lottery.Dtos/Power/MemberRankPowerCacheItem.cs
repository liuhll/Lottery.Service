using System.Collections.Generic;
using Lottery.Infrastructure.Enums;

namespace Lottery.Dtos.Power
{
    public class MemberRankPowerCacheItem
    {
        public const string CacheStoreName = "LotteryRolePowers";

        public string LotteryId { get; set; }

        public MemberRank MemberRank { get; set; }

        public List<string> RoleIds { get; set; }

        public HashSet<string> GrantedPowers { get; set; }

        private MemberRankPowerCacheItem()
        {
            GrantedPowers = new HashSet<string>();
            RoleIds = new List<string>();
        }

        public MemberRankPowerCacheItem(string lotteryId,MemberRank memberRank)
            : this()
        {
            LotteryId = lotteryId;
            MemberRank = memberRank;
        }
    }
}