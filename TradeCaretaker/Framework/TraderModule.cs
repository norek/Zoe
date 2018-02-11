using Autofac;
using TradeCaretaker.Backfill;
using TradeCaretaker.Exchanges;

namespace TradeCaretaker.Framework
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