using Autofac;
using Trader.Backfill;
using Trader.Exchanges;

namespace Trader.Framework
{
    public class TraderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BackFillStorage>().As<IBackfillStorage>().SingleInstance();
            builder.RegisterType<Backfill.Backfill>().As<IBackFill>().SingleInstance();
            builder.RegisterType<Poloniex>().As<IExchange>().SingleInstance();
            
            base.Load(builder);
        }
    }
}
