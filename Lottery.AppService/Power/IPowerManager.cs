using Lottery.Dtos.Menus;
using Lottery.Dtos.Power;
using Lottery.Infrastructure.Enums;
using System.Collections.Generic;

namespace Lottery.AppService.Power
{
    public interface IPowerManager
    {
        PowerDto GetPermission(string powerCode);

        PowerDto GetPermission(string urlPath, string method);

        ICollection<RouteDto> GetUserRoutes(string userId, SystemType systemType);
    }
}