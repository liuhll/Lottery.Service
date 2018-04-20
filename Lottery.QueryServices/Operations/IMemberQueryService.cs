using Lottery.Dtos.Menbers;
using System.Collections.Generic;

namespace Lottery.QueryServices.Operations
{
    public interface IMemberQueryService
    {
        MemberInfoDto GetUserMenberInfo(string userId, string lotteryId);

        ICollection<MemberInfoDto> GetMenberInfos(string lotteryId);
    }
}