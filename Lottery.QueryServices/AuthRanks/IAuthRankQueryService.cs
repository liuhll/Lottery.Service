using System.Collections.Generic;
using Lottery.Dtos.AuthRanks;
using Lottery.Infrastructure.Enums;

namespace Lottery.QueryServices.AuthRanks
{
    public interface IAuthRankQueryService
    {
        AuthRankDto GetAuthRankByLotteryIdAndRank(string lotteryId, MemberRank memberRank);

        ICollection<AuthRankDto> GetAuthRanksByLotteryId(string lotteryId);
    }
}