using System.Security.Claims;

namespace Lottery.Infrastructure.RunTime.Session
{
    public interface IPrincipalAccessor
    {
        ClaimsPrincipal Principal { get; }
    }
}