using System.Collections.Generic;
using System.Threading.Tasks;
using Trader.Exchanges;

namespace Trader.Backfill
{
    public interface IBackfillStorage
    {
        Task CreateIfNotExists(string name);
        Task StoreTrades(string name, IEnumerable<Trade> trades);
    }
}