using ParkingChargeCalculator.Files;
using System;
using System.Collections.Generic;

namespace ParkingChargeCalculator.ParkingChargeCalculators
{
    public class ShortStayParkingChargeCalculator : BaseParkingChargeCalculator
    {
        public override ChargeType ChargeType => ChargeType.ShortStay;
        protected override int ChargeTimeUnit => 1;
        protected override decimal CostPerUnitChargeTimeGbp => 1.1m / 3600;
        protected override IDictionary<DayOfWeek, IntradayTimeSpan> StandardWeekChargableTimePeriods => new Dictionary<DayOfWeek, IntradayTimeSpan>
        {
            { DayOfWeek.Monday, new IntradayTimeSpan(28800, 64800) },
            { DayOfWeek.Tuesday, new IntradayTimeSpan(28800, 64800) },
            { DayOfWeek.Wednesday, new IntradayTimeSpan(28800, 64800) },
            { DayOfWeek.Thursday, new IntradayTimeSpan(28800, 64800) },
            { DayOfWeek.Friday, new IntradayTimeSpan(28800, 64800) }
        };
    }
}