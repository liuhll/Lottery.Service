using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommon.Components;
using ECommon.Dapper;
using Lottery.Core.Caching;
using Lottery.Infrastructure;
using Lottery.QueryServices.Tasks;
using Lottery.QueryServices.Tasks.Dtos;

namespace Lottery.QueryServices.Dapper.Tasks
{
    [Component]
    public class ScheduleTaskQueryService : BaseQueryService, IScheduleTaskQueryService
    {
        private readonly ICacheManager _cacheManager;

        public ScheduleTaskQueryService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public IList<ScheduleTaskInfo> GetAllScheduleTaskInfos()
        {
            using (var conn = GetLotteryConnection())
            {
               return  _cacheManager.Get<IList<ScheduleTaskInfo>>("test", () =>
               {
                   return conn.QueryList<ScheduleTaskInfo>(null, TableNameConstants.ScheduleTaskTable).ToList();
               });
            }
        }
    }
}