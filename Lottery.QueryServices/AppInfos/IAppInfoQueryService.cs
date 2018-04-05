using Lottery.Dtos.AppInfo;
using Lottery.Infrastructure.Enums;

namespace Lottery.QueryServices.AppInfos
{
    public interface IAppInfoQueryService
    {
        AppInfoOutput GetAppInfo(AppPlatform platform);
    }
}