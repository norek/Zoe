using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trader.Backfill;
using Trader.Exchanges;
using Xunit;

namespace Trader.Tests
{
    public class BackfillTests
    {
        private readonly IBackFill _sut;
        private readonly IExchange _exchange;
        private readonly IBackfillStorage _storage;

        public BackfillTests()
        {
            _exchange = Substitute.For<IExchange>();
            _storage = Substitute.For<IBackfillStorage>();

            _sut = new Backfill.Backfill(_exchange, _storage);
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
            await _storage.Received(expecetedCalls).StoreTrades("no_matter", Arg.Any<IEnumerable<Trade>>());
            await _exchange.Received(expecetedCalls).GetTradeHistory("no_matter", Arg.Any<DateTime>(), Arg.Any<DateTime>());
        }
    }
}
