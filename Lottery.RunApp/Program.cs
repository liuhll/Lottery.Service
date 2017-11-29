using System;
using ECommon.Components;
using ECommon.Socketing;
using ENode.Commanding;
using FluentScheduler;
using Lottery.Commands.LotteryDatas;
using Lottery.QueryServices.Dapper.ScheduleTasks;
using Lottery.RunApp.Jobs;

namespace Lottery.RunApp
{
    class Program
    {
        static void Main(string[] args)
        {


            Bootstrap.InitializeFramework();

            //var _commandService = ObjectContainer.Resolve<ICommandService>();

            //var result = _commandService.Execute(
            //    new RunNewLotteryCommand(Guid.NewGuid().ToString(), 10089, "ACB89F4E-7C71-4785-BA09-D7E73084B467",
            //        "1,2,3,4,5,6,7,8,9,10", DateTime.Now), 10000);
            //var taskQueryService = ObjectContainer.Resolve<ScheduleTaskQueryService>();

            //Console.WriteLine(result.Status);

         

            JobManager.Initialize(new JobFactory());

            //Console.WriteLine(result.Status);
            Console.WriteLine(SocketUtils.GetLocalIPV4());

            Console.ReadKey();
        }
    }
}
