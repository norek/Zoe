using Autofac;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RawRabbit.Channel.Abstraction;
using RawRabbit.Configuration;
using RawRabbit.Instantiation;
using RawRabbit.vNext;
using RawRabbit;
using System;
using System.IO;
using System.Text;
using RawRabbit.Extensions;
using RawRabbit.Operations.Subscribe;
using System.Threading.Tasks;
using Trader.Messages;

namespace Trader.ConsoleVisualizer
{
    class Program
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

                Console.ReadLine();

                return Task.FromResult(0);
            }
        }
    }
}
