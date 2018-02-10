using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Trader.Exchanges;
using Microsoft.Extensions.Configuration;
using System.IO;
using Autofac;
using Trader.Framework.Ioc;
using Trader.Framework;
using Trader.Backfill;
using RabbitMQ.Client;
using System.Text;
using RawRabbit;

namespace Trader.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule<ConfigurationModule>();
            builder.RegisterModule<MongoModule>();
            builder.RegisterModule<TraderModule>();

            var container = builder.Build();
            
            using (var scope = container.BeginLifetimeScope())
            {
                var backfill = scope.Resolve<IBackFill>();

                backfill.Execute(new BackFillOptions(DateTime.UtcNow.AddDays(-2), DateTime.UtcNow, "USDT_BTC")).Wait();
            }

            Console.ReadLine();
        }

        static RSI_Strategy strategy = new RSI_Strategy();
        static LinkedList<Trade> _trades = new LinkedList<Trade>();
        private static void SystemStub()
        {

            Poloniex pol = new Poloniex();
            var tradeHistory = pol.GetTradeHistory("USDT_BTC", DateTime.UtcNow.AddDays(-1), DateTime.UtcNow).Result;

            var trades = tradeHistory.Reverse();
            int period = 2;
            DateTime startDate = trades.First().Date;
            DateTime periodDate = startDate;

            strategy.Buy += Strategy_Buy;
            strategy.Sell += Strategy_Sell;

            foreach (var trade in trades)
            {

                if (periodDate < trade.Date)
                {
                    Task.Delay(300).Wait();

                    periodDate = periodDate.AddMinutes(period);

                    OnPeriod(trade);
                }
            }
        }

        private static void Strategy_Sell(object sender, EventArgs e)
        {
            Console.Write("   Sell");
        }

        private static void Strategy_Buy(object sender, EventArgs e)
        {
            Console.Write("   Buy");
        }

        private static void OnPeriod(Trade trade)
        {
            _trades.AddLast(trade);

            if (_trades.Count > 100)
            {
                _trades.RemoveFirst();
            }

            Report(trade);

            strategy.Execute(_trades.ToArray());

            Console.Write(" " + strategy.ToString());
        }

        private static void Report(Trade trade)
        {
            Console.ResetColor();
            Console.WriteLine();
            Console.Write(trade.Date + " ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(trade.Amount + " ");
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.Write(trade.Rate.ToString().PadLeft(12));
        }
    }
}

/*
 * Period
 *  => Strategry calculation
 *  => if filled 
 *      -> buy signal / sell
 *              get balacne
 *              calculate avaiable quant
 *              set stop loss and take profit
 *              push but/stop
 * 
 */
