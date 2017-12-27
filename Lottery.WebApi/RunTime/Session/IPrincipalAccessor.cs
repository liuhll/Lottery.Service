using System.Security.Claims;

namespace Lottery.WebApi.RunTime.Session
{
    public interface IPrincipalAccessor
    {
        ClaimsPrincipal Principal { get; }
    }
}