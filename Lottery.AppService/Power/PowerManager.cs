using ECommon.Components;
using Lottery.Dtos.Power;
using Lottery.QueryServices.Powers;

namespace Lottery.AppService.Power
{
    [Component]
    public class PowerManager : IPowerManager
    {
        private readonly IPowerQueryService _powerQueryService;

        public PowerManager(IPowerQueryService powerQueryService)
        {
            _powerQueryService = powerQueryService;
        }

        public PowerDto GetPermission(string powerCode)
        {
            return _powerQueryService.GetPermissionByCode(powerCode);
        }

        public PowerDto GetPermission(string urlPath, string method)
        {
            return _powerQueryService.GetPermissionByApi(urlPath,method);
        }
    }
}