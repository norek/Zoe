using System.Collections.Generic;
using System.Threading.Tasks;
using TradeCaretaker.Exchanges;

namespace TradeCaretaker.Backfill
{
    public interface IBackfillStorage
    {
        Task CreateIfNotExists(string name);
        Task StoreTrades(string name, IEnumerable<Trade> trades);
    }
}