using ECommon.Components;
using Lottery.Dtos.Menbers;
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

        public string ConcludeUserMemRank(string userId, string systemTypeId)
        {
            if (systemTypeId == LotteryConstants.BackOfficeKey)
            {
                return MemberRank.Ordinary.ToString();
            }
            var userMember = _memberService.GetUserMenberInfo(userId, systemTypeId);
            if (userMember == null)
            {
                return MemberRank.Ordinary.ToString();
            }

            return ((MemberRank)userMember.ComputeMemberRank()).ToString();
        }

        public MemberRank GetUserMemRank(string userId, string systemTypeId)
        {
            if (systemTypeId == LotteryConstants.BackOfficeKey)
            {
                return MemberRank.Ordinary;
            }
            var userMember = _memberService.GetUserMenberInfo(userId, systemTypeId);
            if (userMember == null)
            {
                return MemberRank.Ordinary;
            }
            return userMember.ComputeMemberRank();
        }
    }
}