using Lottery.Infrastructure.Enums;

namespace Lottery.AppService.Authorize
{
    public interface ISystemTypeAuthorizeAttribute
    {
        SystemType[] ClientTypes { get; }
    }
}