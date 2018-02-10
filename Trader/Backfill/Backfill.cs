using RawRabbit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trader.Exchanges;
using Trader.Messages;

namespace Trader.Backfill
{
    class Backfill : IBackFill
    {
        private readonly IExchange _exchange;
        private readonly IBackfillStorage _storage;
        private readonly IBusClient _bus;
        private readonly int SIZE_OF_BIN = 6;

        public Backfill(IExchange exchange, IBackfillStorage storage, IBusClient bus)
        {
            _exchange = exchange;
            _storage = storage;
            _bus = bus;
        }

        public async Task Execute(BackFillOptions options)
        {
            var periodDifference = (int)(options.DateTo - options.DateFrom).TotalHours;

            var bins = (int)Math.Ceiling((decimal)periodDifference / SIZE_OF_BIN);

            if (bins == 0)
            {
                bins = 1;
            }

            await _storage.CreateIfNotExists(options.Asset);

            DateTime dateFrom = options.DateFrom;
            DateTime dateTo = options.DateFrom;

            for (int i = 0; i < bins; i++)
            {
                dateTo = dateTo.AddHours(SIZE_OF_BIN);

                if (dateTo > options.DateTo)
                {
                    dateTo = options.DateTo;
                }

                IEnumerable<Trade> trades = await _exchange.GetTradeHistory(options.Asset, dateFrom, dateTo);
                await _storage.StoreTrades(options.Asset, trades);

                dateFrom = dateTo.AddMilliseconds(1);

                await _bus.PublishAsync(new BackFillStageCompleted() { Asset = options.Asset, Current = i + 1, TotalNumber = bins });
            }
        }
    }
}
