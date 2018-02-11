using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RawRabbit;
using TradeCaretaker.Backfill;
using TradeCaretaker.Exchanges;
using TradeCaretaker.Messages;
using Xunit;

namespace TradeCaretaker.Tests
{
    public class BackfillTests
    {
        private readonly IExchange _exchange;
        private readonly IBackfillStorage _storage;
        private readonly IBackFill _sut;
        private readonly IBusClient _busClient;

        public BackfillTests()
        {
            _exchange = Substitute.For<IExchange>();
            _storage = Substitute.For<IBackfillStorage>();
            _busClient = Substitute.For<IBusClient>();

            _sut = new Backfill.Backfill(_exchange, _storage, _busClient);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(3, 1)]
        [InlineData(6, 1)]
        [InlineData(7, 2)]
        [InlineData(13, 3)]
        public async Task Period_Should_Be_Divided_To_bins_of_Size_6_Hours_And_Execute_as_much_Calls_As_Bins
            (int periodDifferenceInHours, int expecetedCalls)
        {
            //arrange
            var dateFrom = DateTime.Now;
            var dateTo = DateTime.Now.AddHours(periodDifferenceInHours);

            //act
            await _sut.Execute(new BackFillOptions(dateFrom, dateTo, "no_matter"));

            //assert
            await _storage.ReceivedWithAnyArgs(expecetedCalls).StoreTrades("no_matter", Arg.Any<IEnumerable<Trade>>());
            await _exchange.ReceivedWithAnyArgs(expecetedCalls)
                .GetTradeHistory("no_matter", Arg.Any<DateTime>(), Arg.Any<DateTime>());

            await _busClient.ReceivedWithAnyArgs(expecetedCalls)
                .PublishAsync(Arg.Any<BackFillStageCompleted>());
        }
    }
}