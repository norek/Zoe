using TradeCaretaker.Exchanges;

namespace TradeCaretaker
{
    internal class Trader
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