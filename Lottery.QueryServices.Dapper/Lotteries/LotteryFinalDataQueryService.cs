﻿using ECommon.Components;
using ECommon.Dapper;
using Lottery.Core.Caching;
using Lottery.Dtos.Lotteries;
using Lottery.Infrastructure;
using Lottery.QueryServices.Lotteries;
using System.Linq;

namespace Lottery.QueryServices.Dapper.Lotteries
{
    [Component]
    public class LotteryFinalDataQueryService : BaseQueryService, ILotteryFinalDataQueryService
    {
        private readonly ICacheManager _cacheManager;

        public LotteryFinalDataQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public LotteryFinalDataDto GetFinalData(string lotteryId)
        {
            var lotteryFinalDataKey = string.Format(RedisKeyConstants.LOTTERY_FINAL_DATA_KEY, lotteryId);
            return _cacheManager.Get(lotteryFinalDataKey, () =>
            {
                using (var conn = GetLotteryConnection())
                {
                    return conn.QueryList<LotteryFinalDataDto>(new { LotteryId = lotteryId }, TableNameConstants.LotteryFinalDataTable).FirstOrDefault();
                }
            });
        }
    }
}