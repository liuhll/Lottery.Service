using System.Linq;
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

        protected IPrincipalAccessor PrincipalAccessor { get; }

        public ClaimsLotterySession()
        {
            PrincipalAccessor = WebApiPrincipalAccessor.Instance;
        }
    }
}