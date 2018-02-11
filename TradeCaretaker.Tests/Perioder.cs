using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeCaretaker.Exchanges;
using Xunit;

namespace TradeCaretaker.Tests
{
    public class PerioderTests
    {
        [Fact]
        public async Task Should_Return_Latest_Trade_From_Period_When_All_Data_Match_In_One_Period()
        {
            //arrange
            int periodLength = 10;
            DateTime periodStart = new DateTime(2018, 10, 10, 10, 10, 0);
            DateTime periodEnd = periodStart.AddMinutes(periodLength);

            var expected = new Trade() { Date = periodStart.AddMinutes(3), Rate = 1 };

            Trade[] trades = new Trade[3]
            {
                new Trade() { Date = periodStart.AddMinutes(1),Rate = 14},
                new Trade() { Date = periodStart.AddMinutes(2),Rate = 122 },
                expected
            };

            Perioder perioder = new Perioder();
            int numberOfexecution = 0;

            //act
            await perioder.Periodify(trades, periodStart, periodEnd, periodLength, OnPeriod);

            //assert
            Assert.Equal(1, numberOfexecution);

            Task OnPeriod(DateTime periodDate, Trade obj)
            {
                Assert.Equal(expected, obj);
                return Task.FromResult(numberOfexecution++);
            }
        }

        [Fact]
        public async Task Should_Return_Latest_Trade_From_Period_For_Each_Period()
        {
            //arrange
            int periodLength = 10;
            DateTime periodStart = new DateTime(2018, 10, 10, 10, 10, 0);
            DateTime periodEnd = periodStart.AddMinutes(3 * periodLength);


            Trade[] trades = {
                new Trade() { Date = periodStart.AddMinutes(1),Rate = 14},
                new Trade() { Date = periodStart.AddMinutes(4),Rate = 122 },
                new Trade() { Date = periodStart.AddMinutes(8), Rate = 1 },
                //p
                new Trade() { Date = periodStart.AddMinutes(12), Rate = 2 },
                new Trade() { Date = periodStart.AddMinutes(13), Rate = 3 },
                new Trade() { Date = periodStart.AddMinutes(14), Rate = 4 },
                //p
                new Trade() { Date = periodStart.AddMinutes(21), Rate = 5 },
                new Trade() { Date = periodStart.AddMinutes(22), Rate = 6 }
            };

            Trade[] expectedOnPeriod = {
                new Trade() { Date = periodStart.AddMinutes(8), Rate = 1 },
                //p
                new Trade() { Date = periodStart.AddMinutes(14), Rate = 4 },
                //p
                new Trade() { Date = periodStart.AddMinutes(22), Rate = 6 }
            };

            Perioder perioder = new Perioder();
            int numberOfexecution = 0;

            //act
            await perioder.Periodify(trades, periodStart, periodEnd, periodLength, OnPeriod);

            //assert
            Assert.Equal(3, numberOfexecution);

            Task OnPeriod(DateTime periodDate, Trade obj)
            {
                numberOfexecution++;
                Assert.Equal(expectedOnPeriod[numberOfexecution - 1], obj);
                return Task.FromResult(2);
            }
        }

        [Fact]
        public async Task If_In_Period_Is_Empty_Return_Previous_Trade()
        {
            //arrange
            int periodLength = 10;
            DateTime periodStart = new DateTime(2018, 10, 10, 10, 10, 0);
            DateTime periodEnd = periodStart.AddMinutes(3 * periodLength);


            Trade[] trades = {
                new Trade() { Date = periodStart.AddMinutes(1),Rate = 14},
                new Trade() { Date = periodStart.AddMinutes(4),Rate = 122 },
                new Trade() { Date = periodStart.AddMinutes(8), Rate = 1 },
                //p
                //new Trade() { Date = periodStart.AddMinutes(12), Rate = 2 },
                //new Trade() { Date = periodStart.AddMinutes(13), Rate = 3 },
                //new Trade() { Date = periodStart.AddMinutes(14), Rate = 4 },
                //p
                new Trade() { Date = periodStart.AddMinutes(21), Rate = 5 },
                new Trade() { Date = periodStart.AddMinutes(22), Rate = 6 }
            };

            Trade[] expectedOnPeriod = {
                new Trade() { Date = periodStart.AddMinutes(8), Rate = 1 },
                //p
                new Trade() { Date = periodStart.AddMinutes(8), Rate = 1 },
                //p
                new Trade() { Date = periodStart.AddMinutes(22), Rate = 6 }
            };

            Perioder perioder = new Perioder();
            int numberOfexecution = 0;

            //act
            await perioder.Periodify(trades, periodStart, periodEnd, periodLength, OnPeriod);

            //assert
            Assert.Equal(3, numberOfexecution);

            Task OnPeriod(DateTime periodDate, Trade obj)
            {
                numberOfexecution++;
                Assert.Equal(expectedOnPeriod[numberOfexecution - 1], obj);
                return Task.FromResult(0);
            }
        }
    }
}
