using Dapper;
using ECommon.Components;
using ECommon.Dapper;
using Lottery.Core.Caching;
using Lottery.Dtos.Points;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Enums;
using Lottery.QueryServices.Points;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.QueryServices.Dapper.Points
{
    [Component]
    public class PointQueryService : BaseQueryService, IPointQueryService
    {
        private readonly ICacheManager _cacheManager;

        public PointQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public PointDto GetPointInfoByType(PointType pointType)
        {
            var cacheKey = string.Format(RedisKeyConstants.POINT_TYPE_KEY, pointType);
            return _cacheManager.Get<PointDto>(cacheKey, () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    conn.Open();
                    return conn.QueryList<PointDto>(new { @PointType = pointType }, TableNameConstants.PointTypeTable).First();
                }
            });
        }

        public ICollection<SignedDto> GetUserSigneds(string userId)
        {
            var cacheKey = string.Format(RedisKeyConstants.POINT_USER_RECORD_KEY, userId);
            return _cacheManager.Get<ICollection<SignedDto>>(cacheKey, () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    conn.Open();
                    var sql =
                        @"SELECT a.CreateBy AS UserId, CurrentPeriodStartDate=min(CreateTime),CurrentPeriodEndDate=max(CreateTime),DurationDays=max(id1)-min(id1)+1,
    DistanceLastPeriodDays=case a.id1-a.id2 when -1 then 0 else max(datediff(d,rq2,CreateTime)) end
from (
    select id1=datediff(d,GETDATE(),CreateTime),id2=(select count(1) from [dbo].[MS_PointRecord] where CreateTime <= a.CreateTime),rq2=(select max(CreateTime) from [dbo].[MS_PointRecord] where CreateTime < a.CreateTime),* from [dbo].[MS_PointRecord] a
) a
WHERE a.PointType =2 AND a.OperationType=0 AND a.CreateBy=@UserId
group by a.id1-a.id2,a.CreateBy
ORDER BY CurrentPeriodStartDate DESC";
                    return conn.Query<SignedDto>(sql, new { @UserId = userId }).ToList();
                }
            });
        }

        public SignedDto GetUserLastSined(string userId)
        {
            var userSigneds = GetUserSigneds(userId);
            if (userSigneds != null)
            {
                var lastSignedInfo = GetUserSigneds(userId).FirstOrDefault();
                return lastSignedInfo;
            }
            return null;
        }

        public PointRecordOutput GetTodaySigned(string userId)
        {
            var sql = @"select *,CreateTime as SignedTime from [dbo].[MS_PointRecord]
where CreateTime >=convert(varchar(10),Getdate(),120)
and CreateTime < convert(varchar(10),dateadd(d,1,Getdate()),120)
AND CreateBy=@UserId";
            using (var conn = GetLotteryConnection())
            {
                return conn.Query<PointRecordOutput>(sql, new { @UserId = userId }).FirstOrDefault();
            }
        }

        public ICollection<PointRecordOutput> GetSignedList(string userId)
        {
            var sql =
                @"select TOP 500 *,CreateTime as SignedTime from [dbo].[MS_PointRecord] WHERE CreateBy=@UserId ORDER BY SignedTime DESC";
            var cacheKey = string.Format(RedisKeyConstants.POINT_USER_SIGNEDLIST_KEY, userId);
            return _cacheManager.Get<ICollection<PointRecordOutput>>(cacheKey, () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    conn.Open();
                    return conn.Query<PointRecordOutput>(sql, new { @UserId = userId }).ToList();
                }
            });
        }
    }
}