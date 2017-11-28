using System.Collections.Generic;
using System.Linq;
using ECommon.Components;
using ECommon.Dapper;
using Lottery.Core.Caching;
using Lottery.Infrastructure;
using Lottery.QueryServices.ScheduleTasks;
using Lottery.QueryServices.ScheduleTasks.Dtos;

namespace Lottery.QueryServices.Dapper.ScheduleTasks
{
    [Component]
    public class ScheduleTaskQueryService : BaseQueryService, IScheduleTaskQueryService
    {
        private readonly ICacheManager _cacheManager;

        public ScheduleTaskQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public IList<ScheduleTaskDto> GetAllScheduleTaskInfos()
        {
            using (var conn = GetLotteryConnection())
            {
               return  _cacheManager.Get<IList<ScheduleTaskDto>>(RedisKeyConstants.SCHEDULE_TASK_ALL_KEY, () =>
               {
                   return conn.QueryList<ScheduleTaskDto>(null, TableNameConstants.ScheduleTaskTable).ToList();
               });
            }
        }
    }
}