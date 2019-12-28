using FluentAssertions;
using ParkingChargeCalculator.Util;
using Xunit;

namespace Tests
{
    public class DecimalExtensionMethodsTests
    {
        [Theory]
        [InlineData(99.99, "99.99")]
        [InlineData(99, "99.00")]
        [InlineData(99.1, "99.10")]
        [InlineData(0, "0.00")]
        public void The_To2DpString_method_serializes_decimals_to_2_dp(decimal value, string expectedValue)
        {
            value.To2DpString().Should().Be(expectedValue);
        }
    }
}
