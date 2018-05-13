using FluentScheduler;
using Lottery.RunApp.Jobs;
using System;
using System.Linq;
using System.Threading;
using Topshelf;

namespace Lottery.RunApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int minWorker, minIOC;
            // Get the current settings.
            ThreadPool.GetMinThreads(out minWorker, out minIOC);
            // Change the minimum number of worker threads to four, but
            // keep the old setting for minimum asynchronous I/O
            // completion threads.
            ThreadPool.SetMinThreads(250, minIOC);

            if (args.Any())
            {
                HostFactory.Run(x =>
                {
                    x.Service<LotteryAppCrier>(s =>
                    {
                        Bootstrap.InitializeFramework();
                       
                        JobManager.Initialize(new JobFactory());

                        s.ConstructUsing(() => new LotteryAppCrier());
                        s.WhenStarted((b, h) => b.Start(h));
                        s.WhenStopped((b, h) => b.Stop(h));
                    });

                    x.RunAsLocalSystem();

                    x.SetDescription("Lottery AppServer Service");
                    x.SetDisplayName("LotteryAppServer");
                    x.SetServiceName("LotteryAppeServer");
                });

                Bootstrap.InitializePredictTable();
            }
            else
            {
                Bootstrap.InitializeFramework();
                JobManager.Initialize(new JobFactory());
                Bootstrap.Start();
                Bootstrap.InitializePredictTable();
              
                Console.WriteLine("Press enter to exit...");
                var line = Console.ReadLine();
                while (line != "exit")
                {
                    switch (line)
                    {
                        case "cls":
                            Console.Clear();
                            break;

                        default:
                            return;
                    }
                    line = Console.ReadLine();
                }
            }
        }
    }
}