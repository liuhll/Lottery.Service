using System.Linq;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Extensions;
using Lottery.Infrastructure.RunTime.Security;

namespace Lottery.Infrastructure.RunTime.Session
{
    internal class ClaimsLotterySession : LotterySessionBase
    {
        public override string UserId {
            get
            {

                var userIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == LotteryClaimTypes.UserId);
                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    return null;
                }

                string userId = userIdClaim.Value;

                return userId;
            }
        }

        //public override string TicketId
        //{
        //    get
        //    {
        //        var ticketClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == LotteryClaimTypes.TicketId);
        //        if (string.IsNullOrEmpty(ticketClaim?.Value))
        //        {
        //            return null;
        //        }

        //        string ticketId = ticketClaim.Value;

        //        return ticketId;
        //    }
        //}

        public override string UserName
        {
            get
            {
                var userNameClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == LotteryClaimTypes.UserName);
                if (string.IsNullOrEmpty(userNameClaim?.Value))
                {
                    return null;
                }

                string userName = userNameClaim.Value;
               
                return userName;
            }

        }

        public override string Email {
            get
            {
                var emailClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == LotteryClaimTypes.Email);
                if (string.IsNullOrEmpty(emailClaim?.Value))
                {
                    return null;
                }

                string email = emailClaim.Value;

                return email;
            }
        }

        public override string Phone {
            get
            {
                var phoneClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == LotteryClaimTypes.Phone);
                if (string.IsNullOrEmpty(phoneClaim?.Value))
                {
                    return null;
                }

                string phone = phoneClaim.Value;

                return phone;
            }
        }

        public override string SystemTypeId
        {
            get
            {
                var clientTypeClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == LotteryClaimTypes.SystemType);
                if (string.IsNullOrEmpty(clientTypeClaim?.Value))
                {
                    return null;
                }

                string clientType = clientTypeClaim.Value;

                return clientType;
            }
        }

        public override SystemType SystemType {
            get
            {
                if (SystemTypeId == LotteryConstants.BackOfficeKey)
                {
                    return SystemType.BackOffice;
                }
                if (SystemTypeId == LotteryConstants.OfficialWebsite)
                {
                    return SystemType.OfficialWebsite;
                }
                return SystemType.App;
            }
        }

        public override MemberRank MemberRank
        {
            get
            {
                var memberRankClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == LotteryClaimTypes.MemberRank);
                if (string.IsNullOrEmpty(memberRankClaim?.Value))
                {
                    return MemberRank.Ordinary;
                }

                var clientType = memberRankClaim.Value.ToEnum<MemberRank>();

                return clientType;
            }
        }

        protected IPrincipalAccessor PrincipalAccessor { get; }

        public ClaimsLotterySession()
        {
            PrincipalAccessor = WebApiPrincipalAccessor.Instance;
        }
    }
}