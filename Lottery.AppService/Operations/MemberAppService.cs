using ECommon.Components;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Enums;
using Lottery.QueryServices.Operations;

namespace Lottery.AppService.Operations
{
    [Component]
    public class MemberAppService : IMemberAppService
    {
        private readonly IMemberQueryService _memberService;

        public MemberAppService(IMemberQueryService memberService)
        {
            _memberService = memberService;
        }

        public string ConcludeUserMemRank(string userId, string clientTypeId)
        {
            if (clientTypeId == LotteryConstants.AdminClientKey)
            {
                return MemberRank.Ordinary.ToString();
            }
            var userMember = _memberService.GetUserMenberInfo(userId,clientTypeId);
            if (userMember == null)
            {
                return MemberRank.Ordinary.ToString();
            }
            if (userMember.Status == MemberStatus.Invalid)
            {
                return MemberRank.Ordinary.ToString();
            }
            return ((MemberRank) userMember.Grade).ToString();

        }
    }
}