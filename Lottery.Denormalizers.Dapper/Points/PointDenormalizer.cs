using System;
using System.Threading.Tasks;
using Dapper;
using ECommon.Dapper;
using ECommon.IO;
using ENode.Infrastructure;
using Lottery.Core.Caching;
using Lottery.Core.Domain.Points;
using Lottery.Infrastructure;

namespace Lottery.Denormalizers.Dapper.Points
{
    public class PointDenormalizer : AbstractDenormalizer, IMessageHandler<AddPointRecordEvent>
    {
        private readonly ICacheManager _cacheManager;

        public PointDenormalizer(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<AsyncTaskResult> HandleAsync(AddPointRecordEvent evnt)
        {
            var cacheKey1 = string.Format(RedisKeyConstants.POINT_USER_RECORD_KEY, evnt.CreateBy);
            var cacheKey2 = string.Format(RedisKeyConstants.POINT_USER_SIGNEDLIST_KEY, evnt.CreateBy);
            _cacheManager.Remove(cacheKey1);
            _cacheManager.Remove(cacheKey2);
            using (var conn = GetLotteryConnection())
            {
                try
                {
                    conn.Open();
                    var trans = conn.BeginTransaction();
                    var sql1 = "UPDATE dbo.F_UserInfo SET Points=Points+@Point WHERE Id=@UserId";
                    var sql2 = @"INSERT INTO [dbo].[MS_PointRecord]
           ([Id]
           ,[Point]
           ,[PointType]
           ,[OperationType]
           ,[Notes]
           ,[CreateBy]
           ,[CreateTime])  VALUES
           (@Id,@Point,@PointType,@OperationType,@Notes,@CreateBy,@CreateTime)";

                    await conn.ExecuteAsync(sql1, new { Point = evnt.Point, UserId = evnt.CreateBy }, trans);
                    await conn.ExecuteAsync(sql2, new
                    {
                        Id = evnt.AggregateRootId,
                        evnt.Point,
                        evnt.OperationType,
                        evnt.Notes,
                        evnt.PointType,
                        evnt.CreateBy,
                        CreateTime = evnt.Timestamp

                    }, trans);
                    trans.Commit();
                    return AsyncTaskResult.Success;
                }
                catch (Exception ex)
                {
                    throw new IOException("Insert record failed.", ex);
                }
            }
        }
    }
}