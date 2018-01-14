using System.Threading.Tasks;
using ECommon.Dapper;
using ECommon.IO;
using ENode.Infrastructure;
using Lottery.Core.Caching;
using Lottery.Core.Domain.UserNormDefaultConfig;
using Lottery.Infrastructure;

namespace Lottery.Denormalizers.Dapper.Norms
{
    public class NormDenormalizer : AbstractDenormalizer, IMessageHandler<AddUserNormDefaultConfigEvent>
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
                    evnt.MaxErrortSeries,
                    evnt.MinErrortSeries,
                    evnt.MaxRightSeries,
                    evnt.MinRightSeries,
                    evnt.PlanCycle,
                    evnt.ForecastCount,
                    evnt.UnitHistoryCount,
                    evnt.CustomNumbers,
                    Id = evnt.AggregateRootId,
                    CreateBy = evnt.UserId,
                    CreateTime = evnt.Timestamp,

                }, TableNameConstants.UserNormDefaultConfigTable);
            });
        }
    }
}