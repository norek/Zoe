using System;
using MongoDB.Bson;

namespace Zoe.Exchanges
{
    public class Trade
    {
        public long GlobalTradeID { get; set; }
        public long TradeID { get; set; }

        public ObjectId _id { get; set; }

        public DateTime Date { get; set; }
        public string Type { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal Total { get; set; }
    }
}