using System.Collections.Generic;
using System.Threading.Tasks;
using Lottery.QueryServices.Tasks.Dtos;

namespace Lottery.QueryServices.Tasks
{
    public interface IScheduleTaskQueryService
    {
        IList<ScheduleTaskInfo> GetAllScheduleTaskInfos();
    }
}