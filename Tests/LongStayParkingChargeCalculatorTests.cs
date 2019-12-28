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
    public class LongStayParkingChargeCalculatorTests
    {
        class LongStayParkingChargeInstructionsTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { ChargeType.LongStay, new DateTime(2017, 9, 7, 7, 50, 0), new DateTime(2017, 9, 9, 5, 20, 0), 22.5m }; // Given example
                yield return new object[] { ChargeType.LongStay, new DateTime(2020, 1, 1, 0, 0, 0), new DateTime(2020, 1, 1, 0, 0, 1), 7.5m }; // A single seconds should be 1 day charge
                yield return new object[] { ChargeType.LongStay, new DateTime(2020, 1, 1, 0, 0, 0), new DateTime(2020, 1, 2, 0, 0, 0), 15m }; // Inclusive range - Should be 2 days charge
                yield return new object[] { ChargeType.LongStay, new DateTime(2020, 1, 1, 0, 0, 0), new DateTime(2020, 1, 1, 23, 59, 59), 7.5m };
                yield return new object[] { ChargeType.LongStay, new DateTime(2020, 1, 6, 0, 0, 0), new DateTime(2020, 1, 12, 23, 59, 59), 52.5m }; // Full week
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(LongStayParkingChargeInstructionsTestData))]
        public void The_LongStayParkingChargeCalculator_correctly_calculates_correct_parking_charge(ChargeType chargeType, DateTime chargeStart, DateTime chargeEnd, decimal expectedCost)
        {
            var longStayParkingChargeCalculator = new LongStayParkingChargeCalculator();
            var actualCost = longStayParkingChargeCalculator.CalculateChargeGbp(new ParkingChargeInstruction
            {
                ChargeType = chargeType,
                ChargeStart = chargeStart,
                ChargeEnd = chargeEnd
            });

            actualCost.Should().Be(expectedCost);
        }
    }
}
