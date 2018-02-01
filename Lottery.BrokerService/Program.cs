using System;
using System.Linq;
using ECommon.Components;
using ECommon.Logging;
using Topshelf;

namespace Lottery.BrokerService
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Any())
            {               
                HostFactory.Run(x =>
                {
                    Bootstrap.Initialize();
                    x.Service<BrokerCrier>(s =>
                    {
                        s.ConstructUsing(() => new BrokerCrier());
                        s.WhenStarted((b, h) => b.Start(h));
                        s.WhenStopped((b, h) => b.Stop(h));

                    });

                    x.RunAsLocalSystem();

                    x.SetDescription("Lottery Broker Service");        
                    x.SetDisplayName("LotteryBrokerService");                       
                    x.SetServiceName("LotteryBrokerService");

                    x.OnException(ex =>
                    {
                        var _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(Bootstrap).FullName);
                        _logger.Info(ex.Message);

                    });
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
