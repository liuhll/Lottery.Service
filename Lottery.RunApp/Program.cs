using System;
using System.Runtime.InteropServices;
using ECommon.Components;
using ENode.Commanding;
using Lottery.Commands.LotteryDatas;

namespace Lottery.RunApp
{
    class Program
    {
        static void Main(string[] args)
        {
          

            Bootstrap.InitializeFramework();

            var _commandService = ObjectContainer.Resolve<ICommandService>();

           _commandService.ExecuteAsync(
                new RunNewLotteryCommand(Guid.NewGuid().ToString(), 10089, "ACB89F4E-7C71-4785-BA09-D7E73084B467",
                    "1,2,3,4,5,6,7,8,9,10", DateTime.Now)).Wait();
            Console.WriteLine("insert data");

            Console.ReadKey();
        }
    }
}
