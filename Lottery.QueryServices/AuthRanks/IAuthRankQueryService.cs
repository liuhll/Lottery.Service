using Lottery.Dtos.AuthRanks;
using Lottery.Infrastructure.Enums;
using System.Collections.Generic;

namespace Lottery.QueryServices.AuthRanks
{
    public interface IAuthRankQueryService
    {
        AuthRankDto GetAuthRankByLotteryIdAndRank(string lotteryId, MemberRank memberRank);

        ICollection<AuthRankDto> GetAuthRanksByLotteryId(string lotteryId);
    }
}