using RawRabbit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zoe.Backfill;
using Zoe.Exchanges;
using Zoe.Messages;
using Zoe.Strategies;

namespace Zoe.Sim
{
    internal class Simulation : ISimulation
    {
        private readonly IBusClient _bus;
        private readonly IBackfillStorage _storage;
        private readonly LinkedList<Trade> _buffer = new LinkedList<Trade>();

        private RSI_Strategy _strategy = new RSI_Strategy();
        public int BUFFER_LENGTH = 100;

        private readonly Perioder perioder = new Perioder();

        public Simulation(IBackfillStorage storage, IBusClient bus)
        {
            _storage = storage;
            _bus = bus;
        }

        public async Task Run(SimulationOptions options)
        {
            var trades = await _storage.GetTrades(options.Asset, options.DateFrom, options.DateTo);

            //TODO:solve this to array
            await perioder.Periodify(trades.ToArray(), options.DateFrom, options.DateTo, options.PeriodLength,
                internalOnPeriod);

            async Task internalOnPeriod(DateTime periodDate, Trade trade)
            {
                _buffer.AddFirst(trade);

                if (_buffer.Count > BUFFER_LENGTH) _buffer.RemoveLast();

                await OnPeriod(periodDate, trade);
            }
        }

        private async Task OnPeriod(DateTime periodDate, Trade trade)
        {
            if (trade == null)
                await _bus.PublishAsync(new PeriodClosed
                {
                    DateTime = periodDate
                });
            else
                await _bus.PublishAsync(new PeriodClosed
                {
                    DateTime = periodDate,
                    Price = trade.Rate,
                    Quantity = trade.Amount
                });


            //TODO: remove this to array - change linked list as array buffer
            //_strategy.Execute(_buffer.ToArray().AsSpan());
        }
    }
}