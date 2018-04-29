using System;
using System.Linq;
using Topshelf;

namespace Lottery.NameServerService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Any())
            {
                HostFactory.Run(x =>
                {
                    x.Service<NameServerCrier>(s =>
                    {
                        Bootstrap.Initialize();
                        s.ConstructUsing(() => new NameServerCrier());
                        s.WhenStarted((b, h) => b.Start(h));
                        s.WhenStopped((b, h) => b.Stop(h));
                    });

                    x.RunAsLocalSystem();

                    x.SetDescription("Lottery NameServer Service");
                    x.SetDisplayName("LotteryNameServer");
                    x.SetServiceName("LotteryNameServer");
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