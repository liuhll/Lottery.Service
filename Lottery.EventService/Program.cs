﻿using System;
using System.Linq;
using Topshelf;

namespace Lottery.EventService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Any())
            {
                HostFactory.Run(x =>
                {
                    Bootstrap.Initialize();
                    x.Service<EventServiceCrier>(s =>
                    {
                        s.ConstructUsing(() => new EventServiceCrier());
                        s.WhenStarted((b, h) => b.Start(h));
                        s.WhenStopped((b, h) => b.Stop(h));
                    });

                    x.RunAsLocalSystem();

                    x.SetDescription("Lottery EventService");
                    x.SetDisplayName("LotteryEventService");
                    x.SetServiceName("LotteryEventService");
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