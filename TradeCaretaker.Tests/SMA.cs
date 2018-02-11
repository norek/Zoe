using System;
using System.Collections.Generic;
using System.Text;
using TradeCaretaker.Overlays;
using Xunit;

namespace TradeCaretaker.Tests
{
    public class SmaTests
    {

        public static IEnumerable<object[]> TestData
        {
            get
            {
                yield return new object[] { new decimal[] { 1 }, 1, 1 };
                yield return new object[] { new decimal[] { 1, 2 }, 1, 1 };
                yield return new object[] { new decimal[] { 1, 2 }, 2, 1.5m };
                yield return new object[] { new decimal[] { 1, 2, 3 }, 3, 2m };
            }
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Calculate_Success(decimal[] input, int length, decimal expected)
        {
            var result = SMA.CalculateAvarage(input, length);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Empty_Array_Return_Zero()
        {
            var result = SMA.CalculateAvarage(new decimal[0], 1);
            Assert.Equal(0, result);
        }

        [Fact]
        public void Length_Smaller_than_1_Throws_Argument_Exception()
        {
            Assert.Throws<ArgumentException>(() => SMA.CalculateAvarage(new decimal[] { 1, 2, 3 }, 0));
        }

        [Fact]
        public void Length_Grater_Than_Length_Of_Array_Throws_Argument_Exception()
        {
            Assert.Throws<ArgumentException>(() => SMA.CalculateAvarage(new decimal[] { 1, 2, 3 }, 4));
        }
    }
}
