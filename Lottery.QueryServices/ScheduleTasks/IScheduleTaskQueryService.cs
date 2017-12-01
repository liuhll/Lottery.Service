using System.Collections.Generic;
using Lottery.Dtos.ScheduleTasks;

namespace Lottery.QueryServices.ScheduleTasks
{
    public interface IScheduleTaskQueryService
    {
        IList<ScheduleTaskDto> GetAllScheduleTaskInfos();
    }
}