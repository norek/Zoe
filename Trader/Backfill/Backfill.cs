using System.Collections.Generic;
using System.Threading.Tasks;
using Trader.Exchanges;

namespace Trader.Backfill
{
    class Backfill
    {
        private readonly IExchange _exchange;
        private readonly IBackfillStorage _storage;

        public Backfill(IExchange exchange, IBackfillStorage storage)
        {
            _exchange = exchange;
            _storage = storage;
        }

        public async Task Execute(BackFillOptions options)
        {
            IEnumerable<Trade> trades = await _exchange.GetTradeHistory(options.Asset, options.DateFrom, options.DateTo);
            await _storage.StoreTrades(trades);
        }
    }
}
