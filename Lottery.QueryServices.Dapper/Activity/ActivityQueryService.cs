using Dapper;
using ECommon.Components;
using Lottery.Dtos.Activity;
using Lottery.Infrastructure.Enums;
using Lottery.QueryServices.Activities;
using System.Linq;

namespace Lottery.QueryServices.Dapper.Activity
{
    [Component]
    public class ActivityQueryService : BaseQueryService, IActivityQueryService
    {
        public ActivityDto GetAuthAcivity(string authRankId, SellType sellType)
        {
            var sql = "SELECT * FROM [dbo].[S_Activity] WHERE AuthRankId=@AuthRankId AND Status=0";
            using (var conn = GetLotteryConnection())
            {
                conn.Open();
                return conn.Query(sql, new { AuthRankId = authRankId }).FirstOrDefault();
            }
        }
    }
}