using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trader.Exchanges;

namespace Trader
{
    class Trader
    {
        private readonly IExchange _exchange;

        public Trader(IExchange exchange)
        {
            _exchange = exchange;
        }

        public void Buy()
        {

        }

        public void Sell()
        {

        }
    }
}
