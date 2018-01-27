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
            return GetUserConLogs(userId, systemTypeId).Where(p => p.InvalidTime <= DateTime.Now).ToList();
        }

        public ConLogDto GetUserConLog(string userId, string systemTypeId, int clientNo, DateTime invalidTime)
        {
            using (var conn = GetLotteryConnection())
            {
                conn.Open();
                return conn.QueryList<ConLogDto>(new { UserId = userId, SystemTypeId = systemTypeId, ClientNo = clientNo, InvalidTime = invalidTime },
                    TableNameConstants.ConLogTable).FirstOrDefault();
            }
        }

        public ConLogDto GetUserNewestConLog(string userId,string systemTypeId, int clientNo)
        {
            using (var conn = GetLotteryConnection())
            {
                conn.Open();
                var sql = @"SELECT TOP 1 *
                            FROM  [dbo].[F_ConLog] WHERE InvalidTime > GETDATE() AND ClientNo=@ClientNo AND UserId = @UserId
                            AND SystemTypeId=@SystemTypeId AND LogoutTime IS NULL ORDER BY CreateTime DESC";
                return conn.QueryFirstOrDefault<ConLogDto>(sql,new { UserId  = userId, SystemTypeId = systemTypeId, ClientNo = clientNo });
            }
        }
    }
}