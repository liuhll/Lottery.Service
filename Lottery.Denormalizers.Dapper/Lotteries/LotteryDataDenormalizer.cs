using System.Threading.Tasks;
using ECommon.Dapper;
using ECommon.IO;
using ENode.Infrastructure;
using Lottery.Core.Caching;
using Lottery.Core.Domain.LotteryDatas;
using Lottery.Core.Domain.LotteryFinalDatas;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Extensions;

namespace Lottery.Denormalizers.Dapper
{
    public class LotteryDataDenormalizer : AbstractDenormalizer,
        IMessageHandler<LotteryDataAddedEvent>,
        IMessageHandler<UpdateLotteryFinalDataEvent>,
        IMessageHandler<UpdateTodayFirstPeriodEvent>
    {
        private readonly ICacheManager _cacheManager;

        public LotteryDataDenormalizer(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public Task<AsyncTaskResult> HandleAsync(LotteryDataAddedEvent evnt)
        {
            return TryInsertRecordAsync( conn =>
            {
                var lotteryDataRedisKey = string.Format(RedisKeyConstants.LOTTERY_DATA_ALL_KEY,
                    evnt.LotteryDataInfo.LotteryId);
                _cacheManager.Remove(lotteryDataRedisKey);
                return conn.InsertAsync(new
                {
                    Id = evnt.AggregateRootId,
                    evnt.LotteryDataInfo.Data,
                    evnt.LotteryDataInfo.InsertTime,
                    evnt.LotteryDataInfo.LotteryTime,
                    evnt.LotteryDataInfo.Period,
                    evnt.LotteryDataInfo.LotteryId

                }, TableNameConstants.LotteryDataTable);
            });
        }

        public Task<AsyncTaskResult> HandleAsync(UpdateLotteryFinalDataEvent evnt)
        {
            return TryUpdateRecordAsync(conn =>
            {
                var lotteryFianlDataKey = string.Format(RedisKeyConstants.LOTTERY_FINAL_DATA_KEY, evnt.LotteryId);
                _cacheManager.Remove(lotteryFianlDataKey);
                return conn.UpdateAsync(new
                {
                    evnt.Data,
                    evnt.FinalPeriod,
                    evnt.LotteryTime,
                    evnt.UpdateTime
                }, new
                {
                    evnt.LotteryId
                }, TableNameConstants.LotteryFinalDataTable);
            });
        }

        public Task<AsyncTaskResult> HandleAsync(UpdateTodayFirstPeriodEvent evnt)
        {
            return TryUpdateRecordAsync(conn =>
            {
                var lotteryFianlDataKey = string.Format(RedisKeyConstants.LOTTERY_FINAL_DATA_KEY, evnt.LotteryId);
                _cacheManager.Remove(lotteryFianlDataKey);
                return conn.UpdateAsync(new
                {
                    evnt.TodayFirstPeriod,
                    evnt.UpdateTime
                }, new
                {
                    Id = evnt.AggregateRootId 
                }, TableNameConstants.LotteryFinalDataTable);
            });
        }
    }
}