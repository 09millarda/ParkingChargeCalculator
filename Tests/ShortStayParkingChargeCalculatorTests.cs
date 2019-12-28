using FluentAssertions;
using ParkingChargeCalculator.Files;
using ParkingChargeCalculator.Handlers;
using ParkingChargeCalculator.ParkingChargeCalculators;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class ShortStayParkingChargeCalculatorTests
    {
        class ShortStayParkingChargeInstructionsTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { ChargeType.ShortStay, new DateTime(2017, 9, 7, 16, 50, 0), new DateTime(2017, 9, 9, 19, 15, 0), 12.28m }; // Given example
                yield return new object[] { ChargeType.ShortStay, new DateTime(2020, 1, 1, 18, 0, 0), new DateTime(2020, 1, 2, 8, 0, 0), 0m }; // Unchargeable hours
                yield return new object[] { ChargeType.ShortStay, new DateTime(2020, 1, 1, 17, 59, 20), new DateTime(2020, 1, 1, 18, 0, 0), 0.01m }; // 40 seconds = 1p
                yield return new object[] { ChargeType.ShortStay, new DateTime(2020, 1, 1, 17, 59, 59), new DateTime(2020, 1, 1, 20, 0, 0), 0.00m };
                yield return new object[] { ChargeType.ShortStay, new DateTime(2020, 1, 1, 16, 0, 0), new DateTime(2020, 1, 1, 20, 0, 0), 2.2m };
                yield return new object[] { ChargeType.ShortStay, new DateTime(2020, 1, 1, 2, 0, 0), new DateTime(2020, 1, 1, 12, 0, 0), 4.4m };
                yield return new object[] { ChargeType.ShortStay, new DateTime(2020, 1, 1, 6, 0, 0), new DateTime(2020, 1, 1, 8, 0, 1), 0m };
                yield return new object[] { ChargeType.ShortStay, new DateTime(2020, 1, 4, 0, 0, 0), new DateTime(2020, 1, 5, 23, 59, 59), 0m }; // Weekend
                yield return new object[] { ChargeType.ShortStay, new DateTime(2020, 1, 4, 0, 0, 0), new DateTime(2020, 1, 6, 8, 1, 0), 0.02m }; // Weekend plus 1 minute
                yield return new object[] { ChargeType.ShortStay, new DateTime(2020, 1, 6, 0, 0, 0), new DateTime(2020, 1, 12, 23, 59, 59), 55m }; // 1 week

            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(ShortStayParkingChargeInstructionsTestData))]
        public void The_ShortStayParkingChargeCalculator_correctly_calculates_correct_parking_charge(ChargeType chargeType, DateTime chargeStart, DateTime chargeEnd, decimal expectedCost)
        {
            var shortStayParkingChargeCalculator = new ShortStayParkingChargeCalculator();
            var actualCost = shortStayParkingChargeCalculator.CalculateChargeGbp(new ParkingChargeInstruction
            {
                ChargeType = chargeType,
                ChargeStart = chargeStart,
                ChargeEnd = chargeEnd
            });

            actualCost.Should().Be(expectedCost);
        }
    }
}
