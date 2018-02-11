using Autofac;
using Microsoft.Extensions.Configuration;
using RawRabbit;
using RawRabbit.Instantiation;
using System.IO;

namespace TradeCaretaker.Runner
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<IConfiguration>((a, s) =>
            {
                var configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                return configurationBuilder.Build();
            }).SingleInstance();

            builder
                .Register<IInstanceFactory>(c => RawRabbitFactory.CreateInstanceFactory())
                .SingleInstance();
            builder
                .Register<IBusClient>(c => c.Resolve<IInstanceFactory>().Create());
        }
    }
}