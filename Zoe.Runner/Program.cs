using Autofac;
using System;
using Zoe.Backfill;
using Zoe.Framework;
using Zoe.Framework.Ioc;
using Zoe.Sim;

namespace Zoe.Runner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var mode = GetApplicationMode();

            var container = RegisterContainer();

            while (mode != "" || mode != "e")
            {
                using (var scope = container.BeginLifetimeScope())
                {
                    switch (mode)
                    {
                        case "backfill":
                        case "b":
                            Backfill(scope);
                            break;
                        case "simulation":
                        case "s":
                            Simulation(scope);
                            break;
                        default:
                            Console.WriteLine("wrong params: exit e");
                            break;
                    }
                }

                mode = Console.ReadLine();
            }

        }

        private static void Backfill(ILifetimeScope scope)
        {
            var backfill = scope.Resolve<IBackFill>();
            backfill.Execute(new BackFillOptions(DateTime.UtcNow.AddDays(-2), DateTime.UtcNow, "USDT_BTC")).Wait();
        }

        private static void Simulation(ILifetimeScope scope)
        {
            var simulation = scope.Resolve<ISimulation>();
            simulation.Run(new SimulationOptions
            {
                DateFrom = DateTime.UtcNow.AddDays(-3),
                DateTo = DateTime.UtcNow.AddHours(-20),
                Asset = "USDT_BTC",
                PeriodLength = 10
            });
        }

        private static string GetApplicationMode()
        {
            Console.WriteLine("Choose mode: ");
            Console.WriteLine("   1. BackFill b");
            Console.WriteLine("   2. Simulation s");

            var result = Console.ReadLine();

            return result != null ? result.Trim().ToLower() : string.Empty;
        }

        private static IContainer RegisterContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule<ConfigurationModule>();
            builder.RegisterModule<MongoModule>();
            builder.RegisterModule<TraderModule>();

            var container = builder.Build();
            return container;
        }
    }
}