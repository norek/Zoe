using Autofac;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Trader.Runner
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
        }
    }
}