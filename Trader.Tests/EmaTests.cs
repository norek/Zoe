using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Trader.Tests
{
    public class EmaTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Invalid_Ctor_Period_Throws_ArgumentException(int periods)
        {
            Assert.Throws<ArgumentException>(() => new EMA(periods));
        }

        [Fact]
        public void Default_Ctor_Success()
        {
            EMA rsi = new EMA();
            Assert.True(rsi.CollectingData);
        }

        public static IEnumerable<object[]> TestData
        {
            get
            {
                yield return new object[] { new decimal[]
                {
                    22.2734m,
                    22.1940m,
                    22.0847m,
                    22.1741m,
                    22.1840m,
                    22.1344m,
                    22.2337m,
                    22.4323m,
                    22.2436m,
                    22.2933m,
                    22.1542m,
                    22.3926m,
                    22.3816m,
                    22.6109m,
                    23.3558m
                },10, new decimal[]{ 22.2248m,
                    22.2119m,
                    22.2448m,
                    22.2697m,
                    22.3317m,
                    22.5179m,
                    22.7968m
                } };
            }
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Callculate_Success(decimal[] inputValues, int periods, decimal[] expected)
        {
            //arrange
            EMA ema = new EMA(periods);
            for (int i = 0; i < inputValues.Length - periods; i++)
            {
                //act
                ema.Calculate(inputValues.Skip(i).Take(periods).ToArray());

                //assert
                Assert.Equal(expected[i], ema.Current, 4);
            }
        }
    }
}
