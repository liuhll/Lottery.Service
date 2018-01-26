using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ECommon.Components;
using ECommon.Dapper;
using Lottery.Dtos.ConLog;
using Lottery.Infrastructure;
using Lottery.QueryServices.Canlogs;

namespace Lottery.QueryServices.Dapper.Canlogs
{
    [Component]
    public class ConLogQueryService :BaseQueryService, IConLogQueryService
    {
        public async Task<int> GetUserLoginCount(string userId, string systemTypeId)
        {
            using (var conn = GetLotteryConnection())
            {
                conn.Open();
                var sql = "SELECT COUNT(*) FROM F_Conlog WHERE UserId=@UserId AND SysTemTypeId=@SystemTypeId AND LogoutTime IS null AND InvalidTime > GETDATE()";
                var result = await conn.QueryFirstOrDefaultAsync<int>(sql, new {UserId = userId, SysTemTypeId = systemTypeId});
                return result;

            }
        }

        public ICollection<ConLogDto> GetUserConLogs(string userId, string systemTypeId)
        {
            using (var conn = GetLotteryConnection())
            {
                conn.Open();
                return conn.QueryList<ConLogDto>(new {UserId = userId, SysTemTypeId = systemTypeId},
                    TableNameConstants.ConLogTable).ToList();
            }
        }

        public ICollection<ConLogDto> GetUserInvalidConLogs(string userId, string systemTypeId)
        {
            return GetUserInvalidConLogs(userId, systemTypeId).Where(p => p.InvalidTime <= DateTime.Now).ToList();
        }
    }
}