using Lottery.Dtos.Power;

namespace Lottery.QueryServices.Powers
{
    public interface IPowerQueryService
    {
        PowerDto GetPermissionByCode(string powerCode);
        PowerDto GetPermissionByApi(string urlPath, string method);
    }
}