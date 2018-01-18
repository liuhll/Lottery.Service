using System.Collections.Generic;
using Lottery.Dtos.Menbers;

namespace Lottery.QueryServices.Operations
{
    public interface IMemberQueryService
    {
        MemberInfoDto GetUserMenberInfo(string userId, string clientTypeId);

        ICollection<MemberInfoDto> GetMenberInfos(string lotteryId);
    }
}