using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ECommon.Components;
using FluentScheduler;
using Lottery.Dtos.ScheduleTasks;
using Lottery.QueryServices.ScheduleTasks;

namespace Lottery.RunApp.Jobs
{
    public class JobFactory : Registry
    {
        private readonly IScheduleTaskQueryService _scheduleTaskQueryService;

        private readonly ICollection<ScheduleTaskDto> _scheduleTasks;
        
        public JobFactory()
        {
            _scheduleTaskQueryService = ObjectContainer.Resolve<IScheduleTaskQueryService>();
            _scheduleTasks = _scheduleTaskQueryService.GetAllScheduleTaskInfos();
            InitScheduleJob();
        }

        private void InitScheduleJob()
        {
            Debug.Assert(_scheduleTasks != null && _scheduleTasks.Any(),"系统还未设置作业");

            foreach (var task in _scheduleTasks)
            {
                if (task.Enabled)
                {
                    var job = Activator.CreateInstance(Type.GetType(task.Type)) as IJob;
                    Schedule(job)
                        .ToRunNow()
                        .AndEvery(task.Seconds)
                        .Seconds();
                }
               
            }
        }
    }
}