using System.Security.Claims;

namespace Lottery.Infrastructure.RunTime.Security
{
    public static class LotteryClaimTypes
    {
 
        public static string UserName { get; set; } = ClaimTypes.Name;

        public static string UserId { get; set; } = ClaimTypes.NameIdentifier;
        public static string Email { get; set; } = ClaimTypes.Email;
        public static string Phone { get; set; } = ClaimTypes.MobilePhone;

        public static string ClientNo { get; set; } = "http://liuhl.lottery.com/identity/claims/clientNo";

        public static string MemberRank { get; set; } = "http://liuhl.lottery.com/identity/claims/memberrank";

        public static string SystemType { get; set; } = "http://liuhl.lottery.com/identity/claims/systemType";
    }
}