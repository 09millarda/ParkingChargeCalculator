using FluentAssertions;
using Moq;
using ParkingChargeCalculator.Files;
using ParkingChargeCalculator.ParkingChargeCalculators;
using System;
using Xunit;

namespace Tests
{
    public class ParkingChargeCalculatorsFactoryTests
    {
        [Theory]
        [InlineData(ChargeType.LongStay)]
        [InlineData(ChargeType.ShortStay)]
        public void Resolving_IParkingChargeCalculator_from_the_ParkingChargeCalculatorsFactory_works_for_configured_ChargeTypes(ChargeType chargeType)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(sp => sp.GetService(typeof(ShortStayParkingChargeCalculator))).Returns(new ShortStayParkingChargeCalculator());
            serviceProviderMock.Setup(sp => sp.GetService(typeof(LongStayParkingChargeCalculator))).Returns(new LongStayParkingChargeCalculator());

            var factory = new ParkingChargeCalculatorFactory(serviceProviderMock.Object);

            var chargeTypeCalculator = factory.ResolveCalculator(chargeType);

            chargeTypeCalculator.Should().NotBeNull();
            chargeTypeCalculator.ChargeType.Should().Be(chargeType);
        }

        [Fact]
        public void Resolving_IParkingChargeCalculator_from_the_ParkingChargeCalculatorsFactory_with_unconfigured_ChargeType_throws_NotImplementedException()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            var factory = new ParkingChargeCalculatorFactory(serviceProviderMock.Object);

            var invalidChargeType = (ChargeType)Enum.Parse(typeof(ChargeType), "-1");
            factory.Invoking(f => f.ResolveCalculator(invalidChargeType))
                .Should()
                .Throw<NotImplementedException>();
        }
    }
}
