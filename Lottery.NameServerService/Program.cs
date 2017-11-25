using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Lottery.NameServerService
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
                    x.Service<NameServerCrier>(s =>
                    {
                        s.ConstructUsing(() => new NameServerCrier());
                        s.WhenStarted((b, h) => b.Start(h));
                        s.WhenStopped((b, h) => b.Stop(h));
                    });

                    x.RunAsLocalService();

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
