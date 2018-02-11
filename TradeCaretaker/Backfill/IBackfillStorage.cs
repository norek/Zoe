using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeCaretaker.Exchanges;

namespace TradeCaretaker.Backfill
{
    public interface IBackfillStorage
    {
        Task StoreTrades(string name, IEnumerable<Trade> trades);
        Task<List<Trade>> GetTrades(string name, DateTime dateFrom, DateTime dateTo);
    }
}