using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Trader.Exchanges;
using Microsoft.Extensions.Configuration;
using System.IO;
using Autofac;

namespace Trader.Runner
{
    class Program
    {
        static RSI_Strategy strategy = new RSI_Strategy();
        static LinkedList<Trade> _trades = new LinkedList<Trade>();

        static void Main(string[] args)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule<ConfigurationModule>();

            var container = builder.Build();

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


            Console.WriteLine("Hello World!");
            Console.ReadLine();
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
