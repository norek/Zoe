using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Trader.Tests
{
    public class RSITests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Invalid_Ctor_Period_Throws_ArgumentException(int periods)
        {
            Assert.Throws<ArgumentException>(() => new RSI(periods));
        }

        [Fact]
        public void Default_Ctor_Success()
        {
            RSI rsi = new RSI();
            Assert.True(rsi.CollectingData);
        }

        public static IEnumerable<object[]> LessDataTest
        {
            get
            {
                yield return new object[] { new decimal[0], 10 };
                yield return new object[] { new decimal[1] { 1 }, 10 };
                yield return new object[] { new decimal[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 10 };
            }
        }

        [Theory]
        [MemberData(nameof(LessDataTest))]
        public void Less_Data_Then_Periods_Results_CollectionData_And_Empty_Current_Values(decimal[] input, int numberOfPeriods)
        {
            RSI rsi = new RSI(numberOfPeriods);
            rsi.Calculate(input);

            Assert.True(rsi.CollectingData);
            Assert.Empty(rsi.Values);
        }

        public static IEnumerable<object[]> RSIInputData
        {
            get
            {
                yield return new object[] { new decimal[19] {
                    44.3389m,
                    44.0902m,
                    44.1497m,
                    43.6124m,
                    44.3278m,
                    44.8264m,
                    45.0955m,
                    45.4245m,
                    45.8433m,
                    46.0826m,
                    45.8931m,
                    46.0328m,
                    45.6140m,
                    46.2820m,
                    46.2820m,
                    46.0028m,
                    46.0328m,
                    46.4116m,
                    46.2222m}
                , 14 };
            }
        }

        [Theory]
        [MemberData(nameof(RSIInputData))]
        public void Calculate_Success(decimal[] inputStream, int periods)
        {
            RSI rsi = new RSI(periods);

            for (int i = 0; i < inputStream.Length; i++)
            {
                rsi.Calculate(inputStream.Skip(i).Take(periods + 1).ToArray());
            }

            Assert.False(rsi.CollectingData);

            RSI_INDEX firstIndexFromAvarage = new RSI_INDEX()
            {
                AvgGain = 0.2384m,
                AvgLoss = 0.0996m,
                RS = 2.3936m,
                RSI = 70.5328m
            };

            RSI_INDEX secondCalculated = new RSI_INDEX()
            {
                AvgGain = 0.2214m,
                AvgLoss = 0.1124m,
                RS = 1.9690m,
                RSI = 66.3186m
            };

            RSI_INDEX thirdCalculated = new RSI_INDEX()
            {
                AvgGain = 0.2077m,
                AvgLoss = 0.1044m,
                RS = 1.9895m,
                RSI = 66.5498m
            };

            Assert.Equal(firstIndexFromAvarage, rsi.Values.ElementAt(rsi.Values.Count() - 1), RSI_INDEX_Equality.Create(4));
            Assert.Equal(secondCalculated, rsi.Values.ElementAt(rsi.Values.Count() - 2), RSI_INDEX_Equality.Create(4));
            Assert.Equal(thirdCalculated, rsi.Values.ElementAt(rsi.Values.Count() - 3), RSI_INDEX_Equality.Create(4));
        }

        [Fact]
        public void Calculate_Success_On_UpperTrend()
        {
            int periods = 2;
            decimal[] inputStream = new decimal[4] { 1, 2, 3, 4 };

            RSI rsi = new RSI(periods);

            for (int i = 0; i < inputStream.Length; i++)
            {
                rsi.Calculate(inputStream.Skip(i).Take(periods + 1).ToArray());
            }
        }
        [Fact]
        public void Calculate_Success_On_LowerTrend()
        {
            int periods = 2;
            decimal[] inputStream = new decimal[4] { 4, 3, 2, 1 };

            RSI rsi = new RSI(periods);

            for (int i = 0; i < inputStream.Length; i++)
            {
                rsi.Calculate(inputStream.Skip(i).Take(periods + 1).ToArray());
            }
        }
    }
}
