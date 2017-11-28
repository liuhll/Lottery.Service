using System.Collections.Generic;
using Lottery.QueryServices.ScheduleTasks.Dtos;

namespace Lottery.QueryServices.ScheduleTasks
{
    public interface IScheduleTaskQueryService
    {
        IList<ScheduleTaskDto> GetAllScheduleTaskInfos();
    }
}