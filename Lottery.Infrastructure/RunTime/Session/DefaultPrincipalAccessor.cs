using System.Security.Claims;
using System.Threading;

namespace Lottery.Infrastructure.RunTime.Session
{
    public class DefaultPrincipalAccessor : IPrincipalAccessor
    {
        public virtual ClaimsPrincipal Principal => Thread.CurrentPrincipal as ClaimsPrincipal;

        public static DefaultPrincipalAccessor Instance => new DefaultPrincipalAccessor();
    }
}