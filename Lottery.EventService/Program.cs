using System;
using System.Linq;
using Lottery.EventService;
using Topshelf;

namespace Lottery.CommandService
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Any())
            {
                Bootstrap.Initialize();
                HostFactory.Run(x =>
                {
                    x.Service<EventServiceCrier>(s =>
                    {
                        s.ConstructUsing(() => new EventServiceCrier());
                        s.WhenStarted((b, h) => b.Start(h));
                        s.WhenStopped((b, h) => b.Stop(h));
                    });

                    x.RunAsLocalSystem();

                    x.SetDescription("Lottery Command Service");
                    x.SetDisplayName("LotteryCommandService");
                    x.SetServiceName("LotteryCommandService");
                });


            }
            else
            {

                Bootstrap.Initialize();
                Bootstrap.Start();

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
