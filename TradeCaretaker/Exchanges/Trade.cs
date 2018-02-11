using System;

namespace TradeCaretaker.Exchanges
{
    public struct Trade
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal Total { get; set; }
    }
}