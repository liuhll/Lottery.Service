﻿using System.Linq;
using Lottery.WebApi.RunTime.Security;

namespace Lottery.WebApi.RunTime.Session
{
    public class ClaimsLotterySession : LotterySessionBase
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

        protected IPrincipalAccessor PrincipalAccessor { get; }

        public ClaimsLotterySession()
        {
            PrincipalAccessor = WebApiPrincipalAccessor.Instance;
        }
    }
}