using System.Collections.Generic;
using System.Linq;
using ECommon.Components;
using ECommon.Dapper;
using Lottery.Core.Caching;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Collections;
using Lottery.Infrastructure.Enums;
using Lottery.QueryServices.Lotteries;

namespace Lottery.QueryServices.Dapper.Lotteries
{
    [Component]
    public class PositionInfoQueryService : BaseQueryService, IPositionInfoQueryService
    {
        private readonly ICacheManager _cacheManager;

        public PositionInfoQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }


        public ICollection<PositionInfoDto> GetAll()
        {
            return _cacheManager.Get<ICollection<PositionInfoDto>>(RedisKeyConstants.LOTTERY_POSITION_ALL_KEY,
                () =>
                {
                    using (var conn = GetLotteryConnection())
                    {
                        conn.Open();
                        return conn.QueryList<PositionInfoDto>(null, TableNameConstants.PositionInfoTable).Select(p =>
                        {
                            p.NumberType = NumberType.Number;
                            return p;
                        }).ToList();
                    }
                });
        }

        public ICollection<PositionInfoDto> GetLotteryPositions(string lotteryId)
        {
            var redisKey = string.Format(RedisKeyConstants.LOTTERY_POSITION_LOTTERY_KEY, lotteryId);
            return _cacheManager.Get<ICollection<PositionInfoDto>>(redisKey, () =>
            {
                return GetAll().Where(p => p.LotteryId == lotteryId).ToList();
            });
        }
    }
}