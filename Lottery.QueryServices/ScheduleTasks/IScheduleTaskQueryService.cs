using Lottery.Dtos.ScheduleTasks;
using System.Collections.Generic;

namespace Lottery.QueryServices.ScheduleTasks
{
    public interface IScheduleTaskQueryService
    {
        IList<ScheduleTaskDto> GetAllScheduleTaskInfos();
    }
}