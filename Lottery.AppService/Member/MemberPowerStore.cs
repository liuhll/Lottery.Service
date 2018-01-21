using System.Collections.Generic;
using ECommon.Components;
using Lottery.Dtos.Power;
using Lottery.QueryServices.Powers;

namespace Lottery.AppService.Member
{
    [Component]
    public class MemberPowerStore : IMemberPowerStore
    {
        private readonly IMemberPowerQueryService _memberPowerQueryService;

        public MemberPowerStore(IMemberPowerQueryService memberPowerQueryService)
        {
            _memberPowerQueryService = memberPowerQueryService;
        }


        public ICollection<PowerGrantInfo> GetMermberPermissions(string lotteryId, int memberRank)
        {
            return _memberPowerQueryService.GetMermberPermissions(lotteryId, memberRank);
        }
    }
}