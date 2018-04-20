using Lottery.Dtos.Power;
using System.Collections.Generic;

namespace Lottery.QueryServices.Powers
{
    public interface IPowerQueryService
    {
        PowerDto GetPermissionByCode(string powerCode);

        PowerDto GetPermissionByApi(string apiPath, string method);

        ICollection<PowerDto> GetAppPowers();

        ICollection<PowerDto> GetUserBoPowers(string userId);
    }
}