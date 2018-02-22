using Autofac;
using Zoe.Backfill;
using Zoe.Exchanges;
using Zoe.Sim;

namespace Zoe.Framework
{
    public class TraderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BackFillStorage>().As<IBackfillStorage>().SingleInstance();
            builder.RegisterType<Backfill.Backfill>().As<IBackFill>().SingleInstance();
            builder.RegisterType<Poloniex>().As<IExchange>().SingleInstance();
            builder.RegisterType<Simulation>().As<ISimulation>().SingleInstance();

            base.Load(builder);
        }
    }
}