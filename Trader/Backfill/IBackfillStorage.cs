using System.Collections.Generic;
using System.Threading.Tasks;
using Trader.Exchanges;

namespace Trader.Backfill
{
    public interface IBackfillStorage
    {
        Task StoreTrades(IEnumerable<Trade> trades);
    }
}