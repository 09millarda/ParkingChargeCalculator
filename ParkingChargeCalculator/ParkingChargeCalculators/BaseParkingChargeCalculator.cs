using ParkingChargeCalculator.Files;
using ParkingChargeCalculator.Handlers;
using System;
using System.Collections.Generic;

namespace ParkingChargeCalculator.ParkingChargeCalculators
{
    public abstract class BaseParkingChargeCalculator : IParkingChargeCalculator
    {
        public abstract ChargeType ChargeType { get; }
        protected abstract int ChargeTimeUnit { get; }
        protected abstract decimal CostPerUnitChargeTimeGbp { get; }

        protected virtual IDictionary<DayOfWeek, IntradayTimeSpan> StandardWeekChargableTimePeriods { get; } = new Dictionary<DayOfWeek, IntradayTimeSpan>();

        protected bool IsUnitOfTimeChargeable(DateTime dateTimeFrom, int unitOfTimeSeconds)
        {
            if (!StandardWeekChargableTimePeriods.ContainsKey(dateTimeFrom.DayOfWeek)) return false;
            var intradaySpan = StandardWeekChargableTimePeriods[dateTimeFrom.DayOfWeek];

            var secondsIntoDay = (dateTimeFrom - dateTimeFrom.Date).TotalSeconds;
            var endOfTimeUnitSeconds = secondsIntoDay + unitOfTimeSeconds;

            if ((endOfTimeUnitSeconds >= intradaySpan.Start && endOfTimeUnitSeconds <= intradaySpan.End) ||
                (secondsIntoDay >= intradaySpan.Start && secondsIntoDay <= intradaySpan.End))
            {
                return true;
            }

            return false;
        }

        public virtual decimal CalculateChargeGbp(ParkingChargeInstruction instruction)
        {
            var totalPrice = 0m;
            var currentDateTime = instruction.ChargeStart;
            do
            {
                if (IsUnitOfTimeChargeable(currentDateTime, ChargeTimeUnit))
                {
                    totalPrice += CostPerUnitChargeTimeGbp;
                }

                currentDateTime = currentDateTime.AddSeconds(ChargeTimeUnit);
            } while (currentDateTime < instruction.ChargeEnd);

            return Math.Round(totalPrice, 2, MidpointRounding.AwayFromZero);
        }
    }
}
