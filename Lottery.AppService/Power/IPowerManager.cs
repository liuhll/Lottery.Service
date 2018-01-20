using Lottery.Dtos.Power;

namespace Lottery.AppService.Power
{
    public interface IPowerManager
    {
        PowerDto GetPermission(string powerCode);

        PowerDto GetPermission(string urlPath,string method);
    }
}