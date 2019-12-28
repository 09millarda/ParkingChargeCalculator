using ParkingChargeCalculator.Files;
using ParkingChargeCalculator.Handlers;
using System;
using System.Collections.Generic;

namespace ParkingChargeCalculator.ParkingChargeCalculators
{
    public class LongStayParkingChargeCalculator : BaseParkingChargeCalculator
    {
        public override ChargeType ChargeType => ChargeType.LongStay;
        protected override int ChargeTimeUnit => 86400;
        protected override decimal CostPerUnitChargeTimeGbp => 7.5m;
        protected override IDictionary<DayOfWeek, IntradayTimeSpan> StandardWeekChargableTimePeriods => new Dictionary<DayOfWeek, IntradayTimeSpan>
        {
            { DayOfWeek.Monday, new IntradayTimeSpan(1, 86400) },
            { DayOfWeek.Tuesday, new IntradayTimeSpan(1, 86400) },
            { DayOfWeek.Wednesday, new IntradayTimeSpan(1, 86400) },
            { DayOfWeek.Thursday, new IntradayTimeSpan(1, 86400) },
            { DayOfWeek.Friday, new IntradayTimeSpan(1, 86400) },
            { DayOfWeek.Saturday, new IntradayTimeSpan(1, 86400) },
            { DayOfWeek.Sunday, new IntradayTimeSpan(1, 86400) }
        };

        public override decimal CalculateChargeGbp(ParkingChargeInstruction instruction)
        {
            return base.CalculateChargeGbp(new ParkingChargeInstruction
            {
                ChargeType = instruction.ChargeType,
                ChargeStart = instruction.ChargeStart.Date,
                ChargeEnd = instruction.ChargeEnd.Date.AddDays(1).AddMilliseconds(-1)
            });
        }
    }
}