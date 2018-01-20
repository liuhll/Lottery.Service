using System.Linq;
using ECommon.Components;
using ECommon.Dapper;
using Lottery.Dtos.Power;
using Lottery.Infrastructure;
using Lottery.QueryServices.Powers;

namespace Lottery.QueryServices.Dapper.Powers
{
    [Component]
    public class PowerQueryService : BaseQueryService, IPowerQueryService
    {
        public PowerDto GetPermissionByCode(string powerCode)
        {
            using (var conn = GetLotteryConnection())
            {
                return conn.QueryList<PowerDto>(new { PowerCode = powerCode, IsDelete = 0 },TableNameConstants.PowerTable).FirstOrDefault();
            }
        }

        public PowerDto GetPermissionByApi(string urlPath, string method)
        {
            using (var conn = GetLotteryConnection())
            {
                return conn.QueryList<PowerDto>(new { UrlPath = urlPath, HttpMethod = method, IsDelete = 0 }, TableNameConstants.PowerTable).FirstOrDefault();
            }
        }
    }
}