using System.Collections.Generic;
using Lottery.Dtos.Menus;
using Lottery.Dtos.Power;
using Lottery.Infrastructure.Enums;

namespace Lottery.AppService.Power
{
    public interface IPowerManager
    {
        PowerDto GetPermission(string powerCode);

        PowerDto GetPermission(string urlPath,string method);

        ICollection<RouteDto> GetUserRoutes(string userId,SystemType systemType);
    }
}