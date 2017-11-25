﻿using System;
using System.Linq;
using Topshelf;

namespace Lottery.BrokerService
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
                    x.Service<BrokerCrier>(s =>
                    {
                        s.ConstructUsing(() => new BrokerCrier());
                        s.WhenStarted((b, h) => b.Start(h));
                        s.WhenStopped((b, h) => b.Stop(h));
                    });

                    x.RunAsLocalService();

                    x.SetDescription("Lottery Broker Service");        
                    x.SetDisplayName("LotteryBrokerService");                       
                    x.SetServiceName("LotteryBrokerService");
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
