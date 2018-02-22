using Microsoft.Extensions.Configuration;
using RawRabbit;
using RawRabbit.Instantiation;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Zoe.Messages;

namespace Zoe.ConsoleVisualizer
{
    internal class Program
    {
        public static void Main()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfiguration configuration = configurationBuilder.Build();

            RunAsync().GetAwaiter().GetResult();
        }

        public static Task RunAsync()
        {
            using (var client = RawRabbitFactory.CreateSingleton())
            {
                client.SubscribeAsync<BackFillStageCompleted>(async command =>
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(command.Asset);

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"  {command.Current} / {command.TotalNumber}");
                    Console.ResetColor();
                    Console.WriteLine();
                });

                client.SubscribeAsync<PeriodClosed>(async command =>
                {
                    Thread.Sleep(100);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(command.DateTime);

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("   " + Math.Round(command.Price, 4));
                    Console.ResetColor();

                    Console.Write("    " + Math.Round(command.Quantity, 4));
                    Console.WriteLine("   ");

                });

                Console.ReadLine();

                return Task.FromResult(0);
            }
        }
    }
}