using ECommon.Dapper;
using ECommon.IO;
using ENode.Infrastructure;
using Lottery.Core.Caching;
using Lottery.Core.Domain.NormConfigs;
using Lottery.Core.Domain.UserNormDefaultConfig;
using Lottery.Infrastructure;
using System.Threading.Tasks;

namespace Lottery.Denormalizers.Dapper.Norms
{
    public class NormDenormalizer : AbstractDenormalizer, IMessageHandler<AddUserNormDefaultConfigEvent>,
        IMessageHandler<UpdateUserNormDefaultConfigEvent>,
        IMessageHandler<AddNormConfigEvent>,
        IMessageHandler<DeleteNormConfigEvent>,
        IMessageHandler<UpdateNormConfigEvent>
    {
        private readonly ICacheManager _cacheManager;

        public NormDenormalizer(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public Task<AsyncTaskResult> HandleAsync(AddUserNormDefaultConfigEvent evnt)
        {
            var redisKey = string.Format(RedisKeyConstants.LOTTERY_USERNORM_KEY, evnt.LotteryId, evnt.UserId);
            _cacheManager.Remove(redisKey);
            return TryInsertRecordAsync(conn =>
            {
                return conn.InsertAsync(new
                {
                    evnt.LotteryId,
                    evnt.UserId,
                    evnt.ExpectMaxScore,
                    evnt.ExpectMinScore,
                    evnt.LookupPeriodCount,
                    evnt.MaxErrorSeries,
                    evnt.MinErrorSeries,
                    evnt.MaxRightSeries,
                    evnt.MinRightSeries,
                    evnt.PlanCycle,
                    evnt.ForecastCount,
                    evnt.UnitHistoryCount,
                    evnt.HistoryCount,
                    evnt.CustomNumbers,
                    Id = evnt.AggregateRootId,
                    CreateBy = evnt.UserId,
                    CreateTime = evnt.Timestamp,
                }, TableNameConstants.UserNormDefaultConfigTable);
            });
        }

        public Task<AsyncTaskResult> HandleAsync(UpdateUserNormDefaultConfigEvent evnt)
        {
            var redisKey = string.Format(RedisKeyConstants.LOTTERY_USERNORM_KEY, evnt.LotteryId, evnt.UserId);
            _cacheManager.Remove(redisKey);
            return TryUpdateRecordAsync(conn =>
            {
                return conn.UpdateAsync(new
                {
                    evnt.ExpectMaxScore,
                    evnt.ExpectMinScore,
                    evnt.LookupPeriodCount,
                    evnt.MaxErrorSeries,
                    evnt.MinErrorSeries,
                    evnt.MaxRightSeries,
                    evnt.MinRightSeries,
                    evnt.PlanCycle,
                    evnt.ForecastCount,
                    evnt.UnitHistoryCount,
                    evnt.HistoryCount,
                    UpdateBy = evnt.UserId,
                    UpdateTime = evnt.Timestamp,
                }, new { Id = evnt.AggregateRootId }, TableNameConstants.UserNormDefaultConfigTable
                );
            });
        }

        public Task<AsyncTaskResult> HandleAsync(AddNormConfigEvent evnt)
        {
            var redisKey = string.Format(RedisKeyConstants.LOTTERY_NORMCONFIG_LOTTERY_KEY, evnt.LotteryId, evnt.UserId);
            _cacheManager.Remove(redisKey);
            return TryInsertRecordAsync(conn =>
            {
                return conn.InsertAsync(new
                {
                    evnt.LotteryId,
                    evnt.UserId,
                    evnt.PlanId,
                    evnt.LastStartPeriod,
                    evnt.ExpectMaxScore,
                    evnt.ExpectMinScore,
                    evnt.LookupPeriodCount,
                    evnt.MaxErrorSeries,
                    evnt.MinErrorSeries,
                    evnt.MaxRightSeries,
                    evnt.MinRightSeries,
                    evnt.PlanCycle,
                    evnt.ForecastCount,
                    evnt.UnitHistoryCount,
                    HistoryCount = evnt.HistoryCount,
                    evnt.CustomNumbers,
                    evnt.IsDefualt,
                    evnt.IsEnable,
                    evnt.Sort,
                    Id = evnt.AggregateRootId,
                    CreateBy = evnt.UserId,
                    CreateTime = evnt.Timestamp,
                }, TableNameConstants.NormConfigTable);
            });
        }

        public async Task<AsyncTaskResult> HandleAsync(DeleteNormConfigEvent evnt)
        {
            var redisKey = string.Format(RedisKeyConstants.LOTTERY_NORMCONFIG_LOTTERY_KEY, evnt.LotteryId, evnt.UserId);
            _cacheManager.Remove(redisKey);

            var sql = "DELETE FROM dbo.LA_NormConfig WHERE Id=@Id";
            using (var conn = GetLotteryConnection())
            {
                conn.Open();
                await conn.DeleteAsync(new { Id = evnt.AggregateRootId }, TableNameConstants.NormConfigTable);
            }
            return AsyncTaskResult.Success;
        }

        public Task<AsyncTaskResult> HandleAsync(UpdateNormConfigEvent evnt)
        {
            var redisKey = string.Format(RedisKeyConstants.LOTTERY_NORMCONFIG_LOTTERY_KEY, evnt.LotteryId, evnt.UserId);
            _cacheManager.Remove(redisKey);
            return TryUpdateRecordAsync(conn =>
            {
                return conn.UpdateAsync(new
                {
                    evnt.LastStartPeriod,
                    evnt.ExpectMaxScore,
                    evnt.ExpectMinScore,
                    evnt.LookupPeriodCount,
                    evnt.MaxErrorSeries,
                    evnt.MinErrorSeries,
                    evnt.MaxRightSeries,
                    evnt.MinRightSeries,
                    evnt.PlanCycle,
                    evnt.ForecastCount,
                    evnt.UnitHistoryCount,
                    evnt.CustomNumbers,
                    HistoryCount = evnt.HistoryCount,
                    UpdateBy = evnt.UserId,
                    UpdateTime = evnt.Timestamp,
                }, new { Id = evnt.AggregateRootId }, TableNameConstants.NormConfigTable);
            });
        }
    }
}