using Lottery.Infrastructure.Enums;
using System;

namespace Lottery.Dtos.Menbers
{
    public static class MemberInfoExtension
    {
        public static MemberRank ComputeMemberRank(this MemberInfoBase memberInfo)
        {
            if (memberInfo.Status == MemberStatus.Invalid)
            {
                return MemberRank.Ordinary;
            }
            if (memberInfo.InvalidDate < DateTime.Now)
            {
                return MemberRank.Ordinary;
            }
            return (MemberRank)memberInfo.MemberRank;
        }
    }
}