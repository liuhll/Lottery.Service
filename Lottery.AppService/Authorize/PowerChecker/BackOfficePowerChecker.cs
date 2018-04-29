using ECommon.Components;
using Lottery.AppService.Account;

namespace Lottery.AppService.Authorize
{
    [Component]
    public class BackOfficePowerChecker : BasePowerChecker
    {
        public BackOfficePowerChecker(IUserManager userManager) : base(userManager)
        {
        }
    }
}